using LSP.Core.EntityFramework;
using LSP.Dal.Abstract;
using LSP.Dal.Concrete.Context;
using LSP.Entity.Concrete;

namespace LSP.Dal.Concrete.EntityFramework
{
    public class EfLectureDal : EfEntityRepositoryBase<Lecture, LSPDbContext>, ILectureDal
    {
    }
}
