using Google.Authenticator;
using System.Linq.Expressions;
using System.Net;
using LSP.Business.Abstract;
using LSP.Business.Constants;
using LSP.Core.Extensions;
using LSP.Core.Result;
using LSP.Core.Security;
using LSP.Dal.Abstract;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Authentication;
using LSP.Entity.DTO.Configuration;
using LSP.Entity.DTO.UserSecurities;
using LSP.Entity.Enums.Authentication;
using LSP.Entity.Enums.Environment;

namespace LSP.Business.Concrete
{
    public class UserSecurityTypeManager : IUserSecurityTypeService
    {
        private readonly IUserSecurityTypeDal _userSecurityTypeDal;
        private readonly IAccessControlService _accessControlService;
        private readonly IUserService _userService;
        private readonly ISecurityHistoryService _securityHistoryService;
        private readonly IMailService _mailService;
        private readonly AppSettings _appSettings;

        public UserSecurityTypeManager(IUserSecurityTypeDal securitiesDal, IAccessControlService accessControlService, IUserService userService, ISecurityHistoryService securityHistoryService, IMailService mailService, AppSettings appSettings)
        {
            _userSecurityTypeDal = securitiesDal;
            _appSettings = appSettings;
            _accessControlService = accessControlService;
            _userService = userService;
            _securityHistoryService = securityHistoryService;
            _mailService = mailService;
        }

        public IDataResult<UserSecurityType> Add(UserSecurityType c)
        {
            if (c == null)
                return new ErrorDataResult<UserSecurityType>(null, Messages.add_failed_code, Messages.add_failed);

            _userSecurityTypeDal.Add(c);
            return new SuccessDataResult<UserSecurityType>(c, Messages.success, Messages.success);

        }

        public IDataResult<UserSecurityType> Del(UserSecurityType c)
        {
            if (c == null)
                return new ErrorDataResult<UserSecurityType>(null, Messages.delete_failed, Messages.delete_failed_code);

            _userSecurityTypeDal.Delete(c);
            return new SuccessDataResult<UserSecurityType>(c, Messages.success, Messages.success);

        }

        public IDataResult<UserSecurityType> GetById(int id)
        {
            var result = _userSecurityTypeDal.Get(x => x.Id == id);
            if (result != null)
                return new SuccessDataResult<UserSecurityType>(result, Messages.success, Messages.success);

            return new ErrorDataResult<UserSecurityType>(null, "Security Not Found!", Messages.security_not_found);
        }

        public IDataResult<List<UserSecurityType>> GetList()
        {
            var result = _userSecurityTypeDal.GetList().ToList();
            if (result.Count == 0)
                return new SuccessDataResult<List<UserSecurityType>>(result, Messages.success, Messages.success);

            return new ErrorDataResult<List<UserSecurityType>>(result, Messages.security_not_found,
                Messages.security_not_found_code);
        }

        public IDataResult<List<UserSecurityType>> GetListByFilter(Expression<Func<UserSecurityType, bool>> filter)
        {
            var result = _userSecurityTypeDal.GetList(filter).ToList();
            if (result.Count == 0)
                return new SuccessDataResult<List<UserSecurityType>>(result, Messages.success, Messages.success);

            return new ErrorDataResult<List<UserSecurityType>>(result, Messages.security_not_found,
                Messages.security_not_found_code);
        }

        public IDataResult<UserSecurityType> GetByFilter(Expression<Func<UserSecurityType, bool>> filter)
        {
            var result = _userSecurityTypeDal.Get(filter);
            if (result != null)
                return new SuccessDataResult<UserSecurityType>(result, Messages.success, Messages.success);

            return new ErrorDataResult<UserSecurityType>(result, Messages.security_not_found,
                Messages.security_not_found_code);
        }

        public IDataResult<UserSecurityType> Update(UserSecurityType c)
        {
            if (c == null)
                return new ErrorDataResult<UserSecurityType>(null, Messages.update_failed_code, Messages.update_failed);

            _userSecurityTypeDal.Update(c);
            return new SuccessDataResult<UserSecurityType>(c, Messages.success, Messages.success);
        }

        public ServiceResult<bool> UpdateUserSecurity(SecurityRequestDto securityRequestDto)
        {
            var userId = UserIdentityHelper.GetUserId();

            var doSecurityBlockControl =
                Convert.ToBoolean(Environment.GetEnvironmentVariable(KeyEnum.SecurityBlockControl.EnumToString()));

            if (doSecurityBlockControl)
            {
                var securityResult = _accessControlService.SecurityHistoryBlockControl(userId);
                if (!securityResult.Success)
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.Forbidden,
                        Result = new ErrorDataResult<bool>(false, securityResult.Message, securityResult.MessageCode)
                    };
                }
            }

            var checkCodes = _accessControlService.CheckCodes(new SecurityWithUserControlRequestDto() { UserControlCode = null, MfaTypes = securityRequestDto.MfaTypes }, userId);
            if (!checkCodes.Success)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false, checkCodes.Message, checkCodes.MessageCode)
                };
            }

            if (securityRequestDto.MfaTypes.Count >= 2)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false, Messages.cannot_update_more_than_one_security_option, Messages.cannot_update_more_than_one_security_option_code)
                };
            }

            var checkUserSecuritiesCount = _userSecurityTypeDal.GetList(x => x.UserId == userId && x.Status == (byte)SecurityStatusEnums.Active).Count;
            if (checkUserSecuritiesCount == 1)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false, Messages.at_least_one_option_must_be_active, Messages.at_least_one_option_must_be_active_code)
                };
            }

            var securityType = _userSecurityTypeDal.Get(st => st.UserId == userId && st.SecurityType == securityRequestDto.MfaTypes.FirstOrDefault().SecurityType.EnumToString());
            if (securityType == null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false, Messages.not_found_security_type, Messages.not_found_security_type_code)
                };
            }

            if (securityType.Status == 0)
            {
                securityType.Status = (byte)SecurityStatusEnums.Active;
                _userSecurityTypeDal.Update(securityType);
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<bool>(true, Messages.security_type_activated, Messages.security_type_activated_code)
                };
            }
            securityType.Status = (byte)SecurityStatusEnums.Passive;
            _userSecurityTypeDal.Update(securityType);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true, Messages.security_type_deactivated, Messages.security_type_deactivated_code)
            };
        }

        public ServiceResult<List<UserSecuritiesDto>> GetUserSecurities()
        {
            var userId = UserIdentityHelper.GetUserId();
            var getUserSecurity = _userSecurityTypeDal.GetList(s => s.UserId == userId).ToList();
            List<UserSecuritiesDto> userSecurityList = new();
            foreach (var item in getUserSecurity)
            {
                UserSecuritiesDto dto = new()
                {
                    SecurityId = item.Id,
                    SecurityType = item.SecurityType,
                    Status = item.Status,
                };
                userSecurityList.Add(dto);
            }
            return new ServiceResult<List<UserSecuritiesDto>>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<List<UserSecuritiesDto>>(userSecurityList,
                  Messages.success,
                  Messages.success_code)
            };
        }

        public ServiceResult<GoogleAuthenticatorDto> SetupGoogleAuthenticator()
        {
            var userId = UserIdentityHelper.GetUserId();
            var securityType = _userSecurityTypeDal.Get(st => st.UserId == userId && st.SecurityType == MfaTypeEnum.google.EnumToString());

            if (securityType != null)
                return new ServiceResult<GoogleAuthenticatorDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<GoogleAuthenticatorDto>(null, Messages.google_setup_already_done, Messages.google_setup_already_done_code)
                };

            var userEmail = UserIdentityHelper.GetUserEmail();

            var uniqueKeyForUser = userEmail + _appSettings.SecuritySettings.Google2faKey;

            TwoFactorAuthenticator twoFactorAuthenticator = new();
            var setupInfo = twoFactorAuthenticator.GenerateSetupCode(_appSettings.CompanySettings.Name + " ", userEmail, uniqueKeyForUser, false, 3);

            if (securityType == null)
            {
                _userSecurityTypeDal.Add(new UserSecurityType
                {
                    UserId = userId,
                    SecurityType = MfaTypeEnum.google.EnumToString(),
                    CreatedDate = DateTime.Now,
                    Status = (byte)SecurityStatusEnums.Passive
                });
            }

            var result = new GoogleAuthenticatorDto
            {
                ManualEntryKey = setupInfo.ManualEntryKey,
                QrCodeSetupImageUrl = setupInfo.QrCodeSetupImageUrl,
                UserUniqueKey = uniqueKeyForUser
            };

            return new ServiceResult<GoogleAuthenticatorDto>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<GoogleAuthenticatorDto>(result, Messages.success, Messages.success_code)
            };
        }

        public ServiceResult<bool> ToggleGoogleAuthenticator(string code)
        {
            var userEmail = UserIdentityHelper.GetUserEmail();

            string key = userEmail + _appSettings.SecuritySettings.Google2faKey;

            TwoFactorAuthenticator twoFactorAuthenticator = new();
            bool isValid = twoFactorAuthenticator.ValidateTwoFactorPIN(key, code);
            if (!isValid)
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false, Messages.wrong_code, Messages.wrong_code_code)
                };

            var userId = UserIdentityHelper.GetUserId();
            var securityType = _userSecurityTypeDal.Get(st => st.UserId == userId && st.SecurityType == MfaTypeEnum.google.EnumToString());
            if (securityType == null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false, Messages.not_found_security_type, Messages.not_found_security_type_code)
                };
            }

            switch (securityType.Status)
            {
                case (byte)SecurityStatusEnums.Active:
                    securityType.Status = (byte)SecurityStatusEnums.Passive;
                    break;
                case (byte)SecurityStatusEnums.Passive:
                    securityType.Status = (byte)SecurityStatusEnums.Active;
                    break;
            }

            _userSecurityTypeDal.Update(securityType);

            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true, Messages.success, Messages.success_code)
            };
        }

        public ServiceResult<SecurityCodeResponseDto> SendCode(MfaTypeEnum type)
        {
            var userId = UserIdentityHelper.GetUserId();

            var doSecurityBlockControl =
                Convert.ToBoolean(Environment.GetEnvironmentVariable(KeyEnum.SecurityBlockControl.EnumToString()));

            if (doSecurityBlockControl)
            {
                var securityResult = _accessControlService.SecurityHistoryBlockControl(userId);
                if (!securityResult.Success)
                    return new ServiceResult<SecurityCodeResponseDto>()
                    {
                        HttpStatusCode = (short)HttpStatusCode.Forbidden,
                        Result = new ErrorDataResult<SecurityCodeResponseDto>(null,
                            securityResult.Message,
                            securityResult.MessageCode)
                    };
            }

            if (type == MfaTypeEnum.google)
                return new ServiceResult<SecurityCodeResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<SecurityCodeResponseDto>(null, Messages.google_has_own_auth, Messages.google_has_own_auth_code)
                };


            var checkUser = _userService.Get(u => u.Id == userId).Result.Data;
            if (checkUser == null)
            {
                return new ServiceResult<SecurityCodeResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<SecurityCodeResponseDto>(null,
                        Messages.user_not_found,
                        Messages.user_not_found_code)
                };
            }

            var getSecurityType = _userSecurityTypeDal.Get(x =>
                x.UserId == checkUser.Id && string.Equals(x.SecurityType, type.EnumToString()) &&
                x.Status == (byte)SecurityStatusEnums.Active);
            if (getSecurityType == null)
                return new ServiceResult<SecurityCodeResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<SecurityCodeResponseDto>(null,
                        Messages.not_found_security_type,
                        Messages.not_found_security_type_code)
                };

            var securityHistory = _securityHistoryService.GetListByFilter
                (s => s.UserSecurityTypeId == getSecurityType.Id).Data?.MaxBy(s => s.Id);
            if (securityHistory != null)
            {
                if (securityHistory.ExpireDate > DateTime.Now &&
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
                UserId = checkUser.Id,
                UserSecurityTypeId = getSecurityType.Id,
                CreatedDate = DateTime.Now,
                ExpireDate = DateTime.Now.AddMinutes(2),
                SecurityCode = code.ToString()
            };
            _securityHistoryService.Add(newSecurityHistory);

            SecurityCodeResponseDto dto = new()
            {
                ExpireDate = newSecurityHistory.ExpireDate
            };

            switch (type)
            {
                case MfaTypeEnum.email:
                    dto.SentTo = checkUser.Email!;
                    //Mail'in tipine göre değişecek şekilde template'ler oluşturulmalı
                    _mailService.Send(checkUser.Email!, code.ToString(), SmsMailMessages.dear_en);
                    break;
                case MfaTypeEnum.sms:
                    //Sms bilgileri geldiğinde eklenecek.
                    break;
                default:
                    break;
            }

            return new ServiceResult<SecurityCodeResponseDto>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<SecurityCodeResponseDto>(dto,
                    Messages.success,
                    Messages.success_code)
            };
        }
    }
}