using System.Linq.Expressions;
using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Authentication;
using LSP.Entity.DTO.UserSecurities;
using LSP.Entity.Enums.Authentication;

namespace LSP.Business.Abstract
{
    public interface IUserSecurityTypeService
    {
        ServiceResult<List<UserSecuritiesDto>> GetUserSecurities();
        ServiceResult<bool> UpdateUserSecurity(SecurityRequestDto securityRequestDto);
        ServiceResult<GoogleAuthenticatorDto> SetupGoogleAuthenticator();
        ServiceResult<bool> ToggleGoogleAuthenticator(string code);
        ServiceResult<SecurityCodeResponseDto> SendCode(MfaTypeEnum type);

        IDataResult<UserSecurityType> GetById(int id);
        IDataResult<List<UserSecurityType>> GetList();
        IDataResult<List<UserSecurityType>> GetListByFilter(Expression<Func<UserSecurityType, bool>> filter);
        IDataResult<UserSecurityType> GetByFilter(Expression<Func<UserSecurityType, bool>> filter);
        IDataResult<UserSecurityType> Add(UserSecurityType c);
        IDataResult<UserSecurityType> Del(UserSecurityType c);
        IDataResult<UserSecurityType> Update(UserSecurityType c);
    }
}
