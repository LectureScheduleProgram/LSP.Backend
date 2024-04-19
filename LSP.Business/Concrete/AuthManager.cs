using Snickler.EFCore;
using System.Net;
using LSP.Business.Abstract;
using LSP.Business.Constants;
using LSP.Business.Utilities;
using LSP.Business.ValidationRules.FluentValidation.AuthValidators;
using LSP.Core.Aspects.Validation;
using LSP.Core.Entities.Concrete;
using LSP.Core.Extensions;
using LSP.Core.Result;
using LSP.Core.Security;
using LSP.Dal.Concrete.Context;
using LSP.Entity.Concrete;
using LSP.Entity.DTO;
using LSP.Entity.DTO.Authentication;
using LSP.Entity.Enums.Authentication;
using LSP.Entity.Enums.Environment;

namespace LSP.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly ITokenHelper _tokenHelper;
        private readonly IUserSecurityTypeService _securityTypesService;
        private readonly IUserService _usersService;
        private readonly IPasswordHistoryService _passwordHistoriesService;
        private readonly ISecurityHistoryService _securityHistoriesService;
        private readonly IMailService _mailService;
        private readonly IAccessControlService _accessControlService;

        public AuthManager(IUserService usersService, ITokenHelper tokenHelper,
            IUserSecurityTypeService securityTypesService,
            IPasswordHistoryService passwordHistoriesService,
            ISecurityHistoryService securityHistoriesService, IMailService mailService, IAccessControlService accessControlService)
        {
            _tokenHelper = tokenHelper;
            _securityTypesService = securityTypesService;
            _passwordHistoriesService = passwordHistoriesService;
            _securityHistoriesService = securityHistoriesService;
            _mailService = mailService;
            _usersService = usersService;
            _accessControlService = accessControlService;
        }

        [ValidationAspect(typeof(RegisterValidator))]
        public ServiceResult<SecurityResponseDto> Register(RegisterDto registerDto)
        {
            var userResult = _usersService.GetByMail(registerDto.Email);
            if (userResult.Result.Success)
                return new ServiceResult<SecurityResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.Forbidden,
                    Result = new ErrorDataResult<SecurityResponseDto>(null,
                        Messages.already_mail_registered,
                        Messages.already_mail_registered_code)
                };

            HashingHelper.CreatePasswordHash(registerDto.Password, out var passwordSalt, out var passwordHash);

            if (registerDto.Password.Contains(AuthHelper.PlainMail(registerDto.Email)))
                return new ServiceResult<SecurityResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<SecurityResponseDto>(null,
                        Messages.password_cant_contain_email,
                        Messages.password_cant_contain_email_code)
                };

            var user = new User
            {
                Name = string.Empty,
                Surname = string.Empty,
                Email = registerDto.Email,
                CreatedDate = DateTime.Now,
                Status = 1,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                SecurityCode = AuthHelper.RandomString(15),
            };
            _usersService.Add(user);

            #region Security Types
            UserSecurityType userSecurityType = new()
            {
                UserId = user.Id,
                SecurityType = Enum.GetName(typeof(MfaTypeEnum), MfaTypeEnum.email),
                Status = (int)SecurityStatusEnums.Active,
                CreatedDate = DateTime.Now
            };
            _securityTypesService.Add(userSecurityType);

            userSecurityType = new()
            {
                UserId = user.Id,
                SecurityType = Enum.GetName(typeof(MfaTypeEnum), MfaTypeEnum.google),
                Status = (int)SecurityStatusEnums.Passive,
                CreatedDate = DateTime.Now
            };
            _securityTypesService.Add(userSecurityType);
            #endregion

            var getUserSecurities = _accessControlService.GetUserSecurityTypes(user.Id);
            if (!getUserSecurities.Success)
            {
                return new ServiceResult<SecurityResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<SecurityResponseDto>(null, Messages.not_found_security_type,
                        Messages.not_found_security_type_code)
                };
            }

            PasswordHistory passwordHistory = new()
            {
                CreatedDate = DateTime.Now,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                UserId = user.Id
            };
            _passwordHistoriesService.Add(passwordHistory);

            return new ServiceResult<SecurityResponseDto>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<SecurityResponseDto>(getUserSecurities.Data,
                    Messages.success,
                    Messages.success_code)
            };
        }


        [ValidationAspect(typeof(LoginValidator))]
        public ServiceResult<SecurityResponseDto> Login(LoginDto loginDto)
        {
            var userToCheck = _usersService.Get(x => x.Email == loginDto.Email && x.Status != 0);

            if (!userToCheck.Result.Success)
            {
                return new ServiceResult<SecurityResponseDto>()
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<SecurityResponseDto>(null,
                        Messages.user_not_found,
                        Messages.user_not_found_code)
                };
            }

            if (!HashingHelper.VerifyPasswordHash(loginDto.Password, userToCheck.Result.Data.PasswordSalt!,
                    userToCheck.Result.Data.PasswordHash!))
                return new ServiceResult<SecurityResponseDto>()
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<SecurityResponseDto>(null,
                        Messages.wrong_password,
                        Messages.wrong_password_code)
                };

            userToCheck.Result.Data.SecurityCode = AuthHelper.RandomString(15);
            _usersService.Update(userToCheck.Result.Data);

            var doSecurityBlockControl =
                Convert.ToBoolean(Environment.GetEnvironmentVariable(KeyEnum.SecurityBlockControl.EnumToString()));

            if (doSecurityBlockControl)
            {
                var securityResult = _accessControlService.SecurityHistoryBlockControl(userToCheck.Result.Data.Id);
                if (!securityResult.Success)
                    return new ServiceResult<SecurityResponseDto>()
                    {
                        HttpStatusCode = (short)HttpStatusCode.Forbidden,
                        Result = new ErrorDataResult<SecurityResponseDto>(null,
                            securityResult.Message,
                            securityResult.MessageCode)
                    };
            }

            var getUserSecurityTypes = _accessControlService.GetUserSecurityTypes(userToCheck.Result.Data.Id);
            if (!getUserSecurityTypes.Success)
            {
                return new ServiceResult<SecurityResponseDto>()
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new SuccessDataResult<SecurityResponseDto>(getUserSecurityTypes.Data,
                        Messages.not_found_security_type, Messages.not_found_security_type_code)
                };
            }

            return new ServiceResult<SecurityResponseDto>()
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<SecurityResponseDto>(getUserSecurityTypes.Data, Messages.success,
                    Messages.success_code)
            };
        }

        [ValidationAspect(typeof(MfaCodesValidator))]
        public ServiceResult<SecurityCodeResponseDto> SendEmailCode(MfaCodeDto mfaCodeDto)
        {
            var user = _usersService.Get(x =>
                x.SecurityCode!.Trim().Replace(" ", "") == mfaCodeDto.UserControlCode.Trim().Replace(" ", "")).Result.Data;
            if (user is null)
            {
                return new ServiceResult<SecurityCodeResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<SecurityCodeResponseDto>(null,
                        Messages.wrong_code,
                        Messages.wrong_code_code)
                };
            }

            var getSecurityTypes = _securityTypesService.GetByFilter(x =>
                x.UserId == user.Id && string.Equals
                    (x.SecurityType, Enum.GetName(typeof(MfaTypeEnum), MfaTypeEnum.email)) &&
                (x.Status == (byte)SecurityStatusEnums.Active));

            if (!getSecurityTypes.Success)
                return new ServiceResult<SecurityCodeResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<SecurityCodeResponseDto>(null,
                        getSecurityTypes.Message,
                        getSecurityTypes.MessageCode)
                };

            var securityHistoryResult = _securityHistoriesService.GetListByFilter
                (s => s.UserSecurityTypeId == getSecurityTypes.Data!.Id);

            if (!securityHistoryResult.Success)
            {
                var securityHistory = securityHistoryResult.Data!.MaxBy(s => s.Id);

                if (securityHistory is not null &&
                    securityHistory.ExpireDate > DateTime.Now &&
                    securityHistory.Status != (byte)SecurityHistoryStatusEnum.Successfull)
                {
                    return new ServiceResult<SecurityCodeResponseDto>
                    {
                        HttpStatusCode = (short)HttpStatusCode.MethodNotAllowed,
                        Result = new ErrorDataResult<SecurityCodeResponseDto>(null,
                            $"{Messages.wait_for_new_code}, {securityHistory.ExpireDate - DateTime.Now}",
                            Messages.wait_for_new_code_code)
                    };
                }
            }

            Random r = new();
            var code = r.Next(100000, 1000000);

            SecurityHistory newSecurityHistory = new()
            {
                UserId = user.Id,
                UserSecurityTypeId = getSecurityTypes.Data!.Id,
                CreatedDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(2),
                SecurityCode = code.ToString()
            };
            _securityHistoriesService.Add(newSecurityHistory);

            SecurityCodeResponseDto dto = new()
            {
                ExpireDate = newSecurityHistory.ExpireDate,
                SentTo = user.Email!
            };

            //Mail'in tipine göre değişecek şekilde template'ler oluşturulmalı
            _mailService.Send(user.Email!, code.ToString(), SmsMailMessages.dear_en);

            return new ServiceResult<SecurityCodeResponseDto>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<SecurityCodeResponseDto>(dto,
                    Messages.success,
                    Messages.success_code)
            };
        }

        [ValidationAspect(typeof(SecuritiesRequestValidator))]
        public ServiceResult<AccessToken> CheckSecuritiesCode(SecurityWithUserControlRequestDto authSecurityDto)
        {
            if (authSecurityDto.MfaTypes.Count == 0 || string.IsNullOrEmpty(authSecurityDto.UserControlCode))
                return new ServiceResult<AccessToken>()
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<AccessToken>(null,
                        Messages.invalid_value,
                        Messages.invalid_value_code)
                };

            var user = _usersService.Get(x => x.SecurityCode == authSecurityDto.UserControlCode);
            if (!user.Result.Success)
                return new ServiceResult<AccessToken>()
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<AccessToken>(null,
                        Messages.wrong_code,
                        Messages.wrong_code_code)
                };

            var doSecurityBlockControl =
                Convert.ToBoolean(Environment.GetEnvironmentVariable(KeyEnum.SecurityBlockControl.EnumToString()));

            if (doSecurityBlockControl)
            {
                var blockControl = _accessControlService.SecurityHistoryBlockControl(user.Result.Data!.Id);
                if (!blockControl.Success)
                    return new ServiceResult<AccessToken>()
                    {
                        HttpStatusCode = (short)HttpStatusCode.Forbidden,
                        Result = new ErrorDataResult<AccessToken>(null,
                            blockControl.Message,
                            blockControl.MessageCode)
                    };
            }

            var checkCodes = _accessControlService.CheckCodes(authSecurityDto, null);
            if (!checkCodes.Data)
                return new ServiceResult<AccessToken>()
                {
                    //TODO: Check here after finished implementation
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<AccessToken>(null,
                        checkCodes.Message,
                        checkCodes.MessageCode)
                };


            var result = _tokenHelper.CreateJwtToken(user.Result.Data);

            user.Result.Data.SecurityCode = string.Empty;
            _usersService.Update(user.Result.Data);

            return new ServiceResult<AccessToken>()
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<AccessToken>(result,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<SecurityResponseDto> PasswordResetRequest()
        {
            var userId = UserIdentityHelper.GetUserId();
            var user = _usersService.GetById(userId);

            if (user == null)
                return new ServiceResult<SecurityResponseDto>()
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<SecurityResponseDto>(null,
                        Messages.user_not_found,
                        Messages.user_not_found_code)
                };

            user.Result.Data.SecurityCode = AuthHelper.RandomString(15);
            _usersService.Update(user.Result.Data);

            var getSecurities = _accessControlService.GetUserSecurityTypes(userId);

            if (!getSecurities.Success)
                return new ServiceResult<SecurityResponseDto>()
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<SecurityResponseDto>(getSecurities.Data,
                        Messages.user_not_found,
                        Messages.user_not_found_code)
                };

            return new ServiceResult<SecurityResponseDto>()
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<SecurityResponseDto>(getSecurities.Data,
                    Messages.success,
                    Messages.success_code)
            };
        }

        [ValidationAspect(typeof(PasswordResetValidator))]
        public ServiceResult<bool> PasswordReset(PasswordResetDto passwordResetDto)
        {
            var userId = UserIdentityHelper.GetUserId();
            var user = _usersService.GetById(userId);

            if (user == null)
                return new ServiceResult<bool>()
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.user_not_found,
                        Messages.user_not_found_code)
                };

            if (!HashingHelper.VerifyPasswordHash(passwordResetDto.OldPassword, user.Result.Data.PasswordSalt!,
                    user.Result.Data.PasswordHash!))
                return new ServiceResult<bool>()
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.wrong_password,
                        Messages.wrong_password_code)
                };

            if (!string.IsNullOrEmpty(user.Result.Data.Name))
            {
                var getName = user.Result.Data.Name;
                var passwordValidation = getName.Split(' ');

                if (passwordValidation.Any(item =>
                        passwordResetDto.NewPassword.Replace(" ", "").ToLower()
                            .Contains(item.Replace(" ", "").ToLower()) &&
                        item != ""))
                {
                    return new ServiceResult<bool>()
                    {
                        HttpStatusCode = (short)HttpStatusCode.BadRequest,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.password_cant_contain_name,
                            Messages.password_cant_contain_name_code)
                    };
                }
            }

            if (!string.IsNullOrEmpty(user.Result.Data.Surname))
            {
                if (passwordResetDto.NewPassword.Replace(" ", "").ToLower()
                    .Contains(user.Result.Data.Surname.ToLower()))
                {
                    return new ServiceResult<bool>()
                    {
                        HttpStatusCode = (short)HttpStatusCode.BadRequest,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.password_cant_contain_surname,
                            Messages.password_cant_contain_surname_code)
                    };
                }
            }

            if (passwordResetDto.NewPassword.Replace(" ", "").ToLower()
                .Contains(AuthHelper.PlainMail(user.Result.Data.Email).ToLower()))
                return new ServiceResult<bool>()
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.password_cant_contain_email,
                        Messages.password_cant_contain_email_code)
                };

            if (!string.IsNullOrEmpty(user.Result.Data.PhoneNumber))
            {
                if (passwordResetDto.NewPassword.Replace(" ", "")
                .Contains(AuthHelper.Last4DigitOfPhoneNumber(user.Result.Data.PhoneNumber)))
                    return new ServiceResult<bool>()
                    {
                        HttpStatusCode = (short)HttpStatusCode.BadRequest,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.password_cant_contain_last4phone,
                            Messages.password_cant_contain_last4phone_code)
                    };
            }

            if (passwordResetDto.NewPassword != passwordResetDto.CloneNewPassword)
                return new ServiceResult<bool>()
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.wrong_password,
                        Messages.wrong_password_code)
                };

            SecurityWithUserControlRequestDto dto = new()
            {
                UserControlCode = user.Result.Data.SecurityCode,
                MfaTypes = passwordResetDto.MfaTypes
            };

            var checkCodes = _accessControlService.CheckCodes(dto, user.Result.Data.Id);
            if (!checkCodes.Data)
                return new ServiceResult<bool>()
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = checkCodes
                };

            byte[] passwordSalt, passwordHash;

            HashingHelper.CreatePasswordHash(passwordResetDto.NewPassword, out passwordSalt, out passwordHash);

            var passwordCheckResult = _accessControlService.LastThreePasswordCheck(userId, passwordResetDto.NewPassword);
            if (!passwordCheckResult.Success)
                return new ServiceResult<bool>()
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        passwordCheckResult.Message,
                        passwordCheckResult.MessageCode)
                };

            user.Result.Data.PasswordSalt = passwordSalt;
            user.Result.Data.PasswordHash = passwordHash;
            _usersService.Update(user.Result.Data);

            PasswordHistory passwordHistory = new()
            {
                CreatedDate = DateTime.Now,
                PasswordSalt = passwordSalt,
                PasswordHash = passwordHash,
                UserId = userId
            };
            _passwordHistoriesService.Add(passwordHistory);

            //TODO: These lines of codes need to be open after email templates is ready
            // SendMailCodeDto sendMail = new();
            // if (userDetails != null)
            // 	sendMail.ChangePasswordApproveEn(user.Data.Email, SmsMailMessages.dear_en);

            //TODO: Expire Token

            return new ServiceResult<bool>()
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }


        [ValidationAspect(typeof(ForgetPasswordRequestValidator))]
        public ServiceResult<bool> ForgetPassword(ForgetPasswordRequestDto pr)
        {
            var user = _usersService.Get(x =>
                x.SecurityCode.Trim().Replace(" ", "") == pr.UserControlCode.Trim().Replace(" ", "")).Result.Data;
            if (user == null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false, Messages.user_not_found, Messages.user_not_found_code)
                };
            }

            if (!String.IsNullOrEmpty(user.Name))
            {
                var getName = user.Name;
                string[] passwordValidation = getName.Split(' ');
                foreach (var item in passwordValidation)
                {
                    if (pr.NewPassword.ToLower().Contains(item.Replace(" ", "").ToLower()))
                    {
                        return new ServiceResult<bool>
                        {
                            HttpStatusCode = (short)HttpStatusCode.Forbidden,
                            Result = new ErrorDataResult<bool>(false, Messages.password_cant_contain_name,
                                Messages.password_cant_contain_name_code)
                        };
                    }
                }
            }

            if (!String.IsNullOrEmpty(user.Surname) && pr.NewPassword.ToLower().Contains(user.Surname.ToLower()))
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (int)HttpStatusCode.Forbidden,
                    Result = new ErrorDataResult<bool>(false, Messages.password_cant_contain_surname,
                        Messages.password_cant_contain_surname_code)
                };
            }

            if (pr.NewPassword.ToLower().Contains(AuthHelper.PlainMail(user.Email).ToLower()))
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.Forbidden,
                    Result = new ErrorDataResult<bool>(false, Messages.password_cant_contain_email,
                        Messages.password_cant_contain_email_code)
                };

            if (!String.IsNullOrEmpty(user.PhoneNumber))
            {
                if (pr.NewPassword.Contains(AuthHelper.Last4DigitOfPhoneNumber(user.PhoneNumber)))
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.Forbidden,
                        Result = new ErrorDataResult<bool>(false, Messages.password_cant_contain_last4phone,
                            Messages.password_cant_contain_last4phone_code)
                    };
            }

            if (pr.NewPassword != pr.CloneNewPassword)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false, Messages.wrong_password, Messages.wrong_password_code)
                };
            }

            byte[] passwordsalt, passwordhash;
            HashingHelper.CreatePasswordHash(pr.NewPassword, out passwordsalt, out passwordhash);

            var passwordCheckResult = _accessControlService.LastThreePasswordCheck(user.Id, pr.NewPassword);
            if (!passwordCheckResult.Success)
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false, passwordCheckResult.Message,
                        passwordCheckResult.MessageCode)
                };

            user.PasswordSalt = passwordsalt;
            user.PasswordHash = passwordhash;
            user.SecurityCode = null;

            _usersService.Update(user);

            PasswordHistory passwordHistory = new()
            {
                CreatedDate = DateTime.Now,
                PasswordSalt = passwordsalt,
                PasswordHash = passwordhash,
                UserId = user.Id
            };

            _passwordHistoriesService.Add(passwordHistory);

            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true, Messages.success, Messages.success_code)
            };
        }

        public ServiceResult<ForgetPasswordResponseDto> ForgetPasswordRequest(string email)
        {
            var checkUser = _usersService.Get(x => x.Email == email).Result.Data;
            if (checkUser == null)
            {
                return new ServiceResult<ForgetPasswordResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<ForgetPasswordResponseDto>(null, Messages.user_not_found,
                        Messages.user_not_found_code)
                };
            }

            var blockControl = _accessControlService.SecurityHistoryBlockControl(checkUser.Id);
            if (!blockControl.Success)
                return new ServiceResult<ForgetPasswordResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.Forbidden,
                    Result = new ErrorDataResult<ForgetPasswordResponseDto>(null, blockControl.Message,
                        blockControl.MessageCode)
                };

            checkUser.SecurityCode = null;
            checkUser.SecurityCode = AuthHelper.RandomString(15);
            _usersService.Update(checkUser);

            return new ServiceResult<ForgetPasswordResponseDto>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<ForgetPasswordResponseDto>(
                    new ForgetPasswordResponseDto { ControlCode = checkUser.SecurityCode }, Messages.success,
                    Messages.success_code)
            };
        }
    }
}