using LSP.Core.Result;
using LSP.Entity.Concrete;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IFacultyService
    {
        ServiceResult<bool> Add(string name);
        ServiceResult<bool> Update(Faculty Faculty);
        ServiceResult<bool> Delete(int id);
        ServiceResult<Faculty> GetById(short id);
        ServiceResult<List<Faculty>> GetList();
        ServiceResult<Faculty> Get(Expression<Func<Faculty, bool>> filter);
    }
}