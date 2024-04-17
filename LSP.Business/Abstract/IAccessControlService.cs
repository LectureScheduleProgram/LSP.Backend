using LSP.Core.Result;
using LSP.Entity.DTO;
using LSP.Entity.DTO.Authentication;

namespace LSP.Business.Abstract
{
    public interface IAccessControlService
    {
        IDataResult<bool> CheckCodes(SecurityWithUserControlRequestDto authSecurityDto, int? userId);
        IDataResult<SecurityResponseDto> SecurityHistoryBlockControl(int userId);
        IDataResult<bool> LastThreePasswordCheck(int userId, string newPassword);
        IDataResult<SecurityResponseDto> GetUserSecurityTypes(int userId);
    }
}
