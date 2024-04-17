using Google.Authenticator;
using System.Text;
using LSP.Business.Abstract;
using LSP.Business.Constants;
using LSP.Core.Extensions;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using LSP.Entity.Concrete;
using LSP.Entity.DTO;
using LSP.Entity.DTO.Authentication;
using LSP.Entity.DTO.Configuration;
using LSP.Entity.Enums.Authentication;
using LSP.Entity.Enums.Environment;

namespace LSP.Business.Concrete
{
    public class AccessControlManager : IAccessControlService
    {
        private readonly ISecurityHistoryService _securityHistoryService;
        private readonly IUserStatusHistoryService _userStatusHistoryService;
        private readonly IUserService _userService;
        private readonly IUserSecurityTypeDal _userSecurityTypeDal;
        private readonly IPasswordHistoryService _passwordHistoryService;
        private readonly AppSettings _appSettings;

        public AccessControlManager(ISecurityHistoryService securityHistoryService, IUserStatusHistoryService userStatusHistoryService, IUserService userService, IUserSecurityTypeDal userSecurityTypeDal, AppSettings appSettings, IPasswordHistoryService passwordHistoryService)
        {
            _securityHistoryService = securityHistoryService;
            _userStatusHistoryService = userStatusHistoryService;
            _userService = userService;
            _userSecurityTypeDal = userSecurityTypeDal;
            _appSettings = appSettings;
            _passwordHistoryService = passwordHistoryService;
        }

        public IDataResult<bool> CheckCodes(SecurityWithUserControlRequestDto authSecurityDto, int? userId)
        {
            var user = _userService.Get(u => userId == null ? u.SecurityCode == authSecurityDto.UserControlCode : u.Id == userId);
            if (!user.Result.Success)
                return new ErrorDataResult<bool>(false, user.Result.Message, user.Result.MessageCode);

            var doSecurityBlockControl =
                Convert.ToBoolean(Environment.GetEnvironmentVariable(KeyEnum.SecurityBlockControl.EnumToString()));
            var securityResult = SecurityHistoryBlockControl(user.Result.Data!.Id);
            if (!securityResult.Success && doSecurityBlockControl)
                return new ErrorDataResult<bool>(false, securityResult.Message, securityResult.MessageCode);

            if (authSecurityDto.MfaTypes.Count <= 0)
                return new ErrorDataResult<bool>(false, "not found data", Messages.not_found_data);

            var seperationKey = string.Empty;
            var errors = string.Empty;
            while (true)
            {
                seperationKey = Guid.NewGuid().ToString("N");
                var secHistory = _securityHistoryService.GetByFilter(s =>
                    s.Seperation == seperationKey && s.Seperation != string.Empty);
                if (!secHistory.Success)
                    break;
            }

            foreach (var item in authSecurityDto.MfaTypes)
            {
                if (string.IsNullOrEmpty(item.SecurityCode))
                    return new ErrorDataResult<bool>(false, "security code cannot be null",
                        Messages.wrong_code);

                var getSecurities = _userSecurityTypeDal.Get(x =>
                    x.UserId == user.Result.Data.Id &&
                    string.Equals(x.SecurityType, item.SecurityType.EnumToString()) &&
                    (x.Status == (int)SecurityStatusEnums.Active));

                if (getSecurities == null)
                {
                    return new ErrorDataResult<bool>(false, Messages.not_found_security_type, Messages.not_found_security_type_code);
                }

                if (getSecurities.SecurityType == "google")
                {
                    SecurityHistory securityHistory = new()
                    {
                        CreatedDate = DateTime.Now,
                        UserId = user.Result.Data.Id,
                        UserSecurityTypeId = getSecurities.Id,
                        Seperation = seperationKey
                    };
                    var email = user.Result.Data.Email;
                    string key = email + _appSettings.SecuritySettings.Google2faKey;
                    TwoFactorAuthenticator twoFactorAuthenticator = new();
                    bool isValid = twoFactorAuthenticator.ValidateTwoFactorPIN(key, item.SecurityCode);
                    if (!isValid)
                    {
                        securityHistory.Status = (int)SecurityHistoryStatusEnum.Failed;
                        securityHistory.Seperation = seperationKey;
                        _securityHistoryService.Add(securityHistory);
                        if (string.IsNullOrEmpty(errors))
                            errors += getSecurities.SecurityType;
                        else
                            errors += " " + getSecurities.SecurityType;
                    }
                    else
                    {
                        securityHistory.Seperation = seperationKey;
                        securityHistory.Status = (int)SecurityHistoryStatusEnum.Successfull;
                        _securityHistoryService.Add(securityHistory);
                    }
                }
                else
                {
                    var getSecurityHistories = _securityHistoryService
                        .GetListByFilter(s => s.UserSecurityTypeId == getSecurities.Id);

                    if (!getSecurityHistories.Success)
                        return new ErrorDataResult<bool>(false, Messages.security_history_not_found,
                            Messages.security_history_not_found_code);

                    var securityHistory = getSecurityHistories.Data.MaxBy(s => s.Id);

                    if (DateTime.Now > securityHistory.ExpireDate)
                        return new ErrorDataResult<bool>(false,
                            $"Your {getSecurities.SecurityType} code has expired!",
                            Messages.err_code_expired);

                    if (getSecurities.SecurityType == item.SecurityType.EnumToString() &&
                        securityHistory.SecurityCode?.Trim().Replace(" ", "") ==
                        item.SecurityCode.Trim().Replace(" ", ""))
                    {
                        _userSecurityTypeDal.Update(getSecurities);

                        securityHistory.Seperation = seperationKey;

                        if (securityHistory.Status == 0)
                        {
                            securityHistory.Status = (int)SecurityHistoryStatusEnum.Successfull;
                            _securityHistoryService.Update(securityHistory);
                        }
                        else
                        {
                            securityHistory.Id = 0;
                            securityHistory.Status = (int)SecurityHistoryStatusEnum.Successfull;
                            _securityHistoryService.Add(securityHistory);
                        }
                    }
                    else
                    {
                        if (securityHistory.Status == (int)SecurityHistoryStatusEnum.Failed)
                        {
                            SecurityHistory newSecurityHistory = new()
                            {
                                UserId = securityHistory.UserId,
                                UserSecurityTypeId = securityHistory.UserSecurityTypeId,
                                SecurityCode = securityHistory.SecurityCode,
                                Status = securityHistory.Status,
                                CreatedDate = DateTime.Now,
                                ExpireDate = securityHistory.ExpireDate,
                                Seperation = seperationKey
                            };
                            _securityHistoryService.Add(newSecurityHistory);
                        }
                        else
                        {
                            if (securityHistory.Status == 0)
                            {
                                securityHistory.Seperation = seperationKey;
                                securityHistory.Status = (int)SecurityHistoryStatusEnum.Failed;
                                _securityHistoryService.Update(securityHistory);
                            }
                            else
                                _securityHistoryService.Add(securityHistory);
                        }

                        if (string.IsNullOrEmpty(errors))
                            errors += getSecurities.SecurityType;
                        else
                            errors += " " + getSecurities.SecurityType;
                    }
                }
            }

            securityResult = SecurityHistoryBlockControl(user.Result.Data.Id);
            if (!securityResult.Success && doSecurityBlockControl)
                return new ErrorDataResult<bool>(false, securityResult.Message, securityResult.MessageCode);

            if (string.IsNullOrEmpty(errors))
                return new SuccessDataResult<bool>(true, Messages.success, Messages.success_code);
            else
                return new ErrorDataResult<bool>(false, $"wrong code ({errors})", Messages.wrong_code);
        }

        public IDataResult<SecurityResponseDto> SecurityHistoryBlockControl(int userId)
        {
            var lastSuccessHistory = _securityHistoryService
                .GetListByFilter(s => s.UserId == userId && s.Status == (int)UserSecurityHistoryStatusEnum.Successfull)
                .Data
                .LastOrDefault();

            IOrderedEnumerable<SecurityHistory> securityHistories;

            if (lastSuccessHistory != null)
                securityHistories = _securityHistoryService
                    .GetListByFilter(s => s.UserId == userId && s.CreatedDate > lastSuccessHistory.CreatedDate).Data
                    .OrderByDescending(s => s.Id);
            else
                securityHistories = _securityHistoryService.GetListByFilter(s => s.UserId == userId).Data
                    .OrderByDescending(s => s.Id);

            var counter = 0;
            var seperationCode = string.Empty;
            foreach (var securityHistory in securityHistories)
            {
                if (seperationCode == securityHistory.Seperation)
                    continue;
                seperationCode = securityHistory.Seperation;
                var security = securityHistories.Where(s => s.Seperation == seperationCode).ToList();
                if (security.Where(s => s.Status == (int)UserSecurityHistoryStatusEnum.Failed).ToList().Count > 0)
                {
                    counter++;
                    continue;
                }

                if (security.Where(s => s.Status == (int)UserSecurityHistoryStatusEnum.Unused).Any())
                    continue;
                if (security.Where(s => s.Status == (int)UserSecurityHistoryStatusEnum.Successfull).ToList()
                        .Count == security.Count)
                    break;
            }

            if (counter < 3)
                return new SuccessDataResult<SecurityResponseDto>(null, Messages.success, Messages.success_code);
            {
                if (counter > 5)
                    counter %= 5;


                var statusHistory = _userStatusHistoryService.GetListByFilter(s => s.UserId == userId).Data;
                var securityHistory = _securityHistoryService
                    .GetListByFilter(s => s.UserId == userId && s.Status == 2).Data;

                if (counter is >= 5 or 0)
                {
                    var remainingTime = (securityHistory.Max(s => s.CreatedDate).AddHours(6) - DateTime.Now);
                    if (Convert.ToInt64(remainingTime.TotalMilliseconds) <= 0)
                        return new SuccessDataResult<SecurityResponseDto>(null, Messages.success,
                            Messages.success_code);
                    {
                        if (statusHistory.Count > 0)
                        {
                            if (securityHistory.Max(s => s.CreatedDate).AddHours(6) != statusHistory.Last().EndDate)
                            {
                                _userStatusHistoryService.Add(new UserStatusHistory
                                {
                                    UserId = userId,
                                    Description = "Wrong code/codes entered 5 times",
                                    CreatedDate = DateTime.Now,
                                    EndDate = securityHistory.Max(s => s.CreatedDate).AddHours(6)
                                });

                                //TODO Expire Token
                            }
                        }
                        else
                        {
                            _userStatusHistoryService.Add(new UserStatusHistory
                            {
                                UserId = userId,
                                Description = "Wrong code/codes entered 5 times",
                                CreatedDate = DateTime.Now,
                                EndDate = securityHistory.Max(s => s.CreatedDate).AddHours(6)
                            });

                            //TODO Expire Token
                        }


                        return new ErrorDataResult<SecurityResponseDto>(null,
                            $"You entered wrong code/codes 5 times. Needed to wait {remainingTime}",
                            Messages.multiple_wrong_code);
                    }
                }

                if (counter == 4)
                    return new SuccessDataResult<SecurityResponseDto>(null, Messages.success, Messages.success_code);
                {
                    if (counter != 3)
                        return new SuccessDataResult<SecurityResponseDto>(null, Messages.success,
                            Messages.success_code);
                    var remainingTime = (securityHistory.Max(s => s.CreatedDate).AddMinutes(2) - DateTime.Now);
                    if (Convert.ToInt64(remainingTime.TotalMilliseconds) <= 0)
                        return new SuccessDataResult<SecurityResponseDto>(null, Messages.success,
                            Messages.success_code);
                    {
                        if (statusHistory.Count > 0)
                        {
                            if (securityHistory.Max(s => s.ExpireDate) == statusHistory.Last().EndDate)
                                return new ErrorDataResult<SecurityResponseDto>(null,
                                    $"You entered wrong code/codes 3 times. Needed to wait {remainingTime}",
                                    Messages.multiple_wrong_code);
                            {
                                _userStatusHistoryService.Add(new UserStatusHistory
                                {
                                    UserId = userId,
                                    Description = "Wrong code/codes entered 3 times",
                                    CreatedDate = DateTime.Now,
                                    EndDate = securityHistory.Max(s => s.ExpireDate)
                                });

                                //TODO: Expire Token
                                // using var context = new LSPDbContext();
                                // context.LoadStoredProc("dbo.pr_TokenExpire")
                                //     .WithSqlParam("UserId", userId)
                                //     .ExecuteStoredProc((handler) => { });
                            }
                        }
                        else
                        {
                            _userStatusHistoryService.Add(new UserStatusHistory
                            {
                                UserId = userId,
                                Description = "Wrong code/codes entered 3 times",
                                CreatedDate = DateTime.Now,
                                EndDate = securityHistory.Max(s => s.ExpireDate)
                            });

                            //TODO: Expire Token
                            // using var context = new LSPDbContext();
                            // context.LoadStoredProc("dbo.pr_TokenExpire")
                            //     .WithSqlParam("UserId", userId)
                            //     .ExecuteStoredProc((handler) => { });
                        }

                        //TODO Expire Token

                        return new ErrorDataResult<SecurityResponseDto>(null,
                            $"You entered wrong code/codes 3 times. Needed to wait {remainingTime}",
                            Messages.multiple_wrong_code);
                    }
                }
            }
        }

        public IDataResult<bool> LastThreePasswordCheck(int userId, string newPassword)
        {
            var getUserPasswordHistories = _passwordHistoryService.GetList(h => h.UserId == userId);
            if (!getUserPasswordHistories.Success)
                return new ErrorDataResult<bool>(false, getUserPasswordHistories.Message,
                    getUserPasswordHistories.MessageCode);

            var userPasswordHistories = getUserPasswordHistories.Data.OrderBy(h => h.CreatedDate).TakeLast(3);

            foreach (var history in userPasswordHistories)
            {
                using var hmac = new System.Security.Cryptography.HMACSHA512(history.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(newPassword));
                var counter = computedHash.Where((passwordHashByte, i) => passwordHashByte == history.PasswordHash[i])
                    .Count();

                if (counter == 64)
                    return new ErrorDataResult<bool>(false, Messages.passwords_cant_same_last3,
                        Messages.passwords_cant_same_last3_code);
            }

            return new SuccessDataResult<bool>(true, Messages.success, Messages.success_code);
        }

        public IDataResult<SecurityResponseDto> GetUserSecurityTypes(int userId)
        {
            SecurityResponseDto mSec = new();

            var getSecurityTypesOfUser = _userSecurityTypeDal.GetList(x =>
                x.UserId == userId && (x.Status == (byte)SecurityStatusEnums.Active));
            var getuser = _userService.Get(x => x.Id == userId).Result.Data;

            if (getuser.Status == 0)
                return new ErrorDataResult<SecurityResponseDto>(null, Messages.user_not_active,
                    Messages.user_not_active_code);

            //

            var securitySystems = getSecurityTypesOfUser.Select(item => new MfaTypeResponseDto()
            {
                SecurityType = item.SecurityType.StringToEnum<MfaTypeEnum>()
            }).ToList();

            mSec.MfaTypes = securitySystems;
            mSec.UserControlCode = getuser.SecurityCode;

            return new SuccessDataResult<SecurityResponseDto>(mSec, Messages.success, Messages.success_code);
        }
    }
}
