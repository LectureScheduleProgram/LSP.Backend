using LSP.Core.Result;
using LSP.Core.Security;
using LSP.Entity.DTO;
using LSP.Entity.DTO.Authentication;

namespace LSP.Business.Abstract
{
    public interface IAuthService
    {
        ServiceResult<SecurityResponseDto> Register(RegisterDto registerDto);
        ServiceResult<SecurityResponseDto> Login(LoginDto loginDto);
        ServiceResult<AccessToken> CheckSecuritiesCode(SecurityWithUserControlRequestDto dto);
        ServiceResult<SecurityResponseDto> PasswordResetRequest();
        ServiceResult<bool> PasswordReset(PasswordResetDto passwordReset);
        ServiceResult<SecurityCodeResponseDto> SendEmailCode(MfaCodeDto auth);
        ServiceResult<ForgetPasswordResponseDto> ForgetPasswordRequest(string email);
        ServiceResult<bool> ForgetPassword(ForgetPasswordRequestDto pr);
    }
}
