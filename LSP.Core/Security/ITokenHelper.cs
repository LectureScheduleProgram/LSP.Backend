using LSP.Core.Entities.Concrete;
using LSP.Core.Result;

namespace LSP.Core.Security
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User users, List<OperationClaims> operationClaims);
        //SessionAddDto CreateNewToken(Users users);
        IDataResult<SessionAddDto> CreateNewToken(User users);

        AccessToken CreateJwtToken(User users);
        IDataResult<TokenInfo> GetTokenInfo();
    }
}
