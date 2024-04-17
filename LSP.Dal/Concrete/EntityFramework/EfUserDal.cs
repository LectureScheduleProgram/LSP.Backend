using LSP.Core.Entities.Concrete;
using LSP.Core.EntityFramework;
using LSP.Dal.Abstract;
using LSP.Dal.Concrete.Context;

namespace LSP.Dal.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, LSPDbContext>, IUserDal
    {
    }
}
