using LSP.Core.Entities.Concrete;
using LSP.Core.Repository;

namespace LSP.Dal.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        // List<OperationClaims> GetClaims(Users user);
    }
}
