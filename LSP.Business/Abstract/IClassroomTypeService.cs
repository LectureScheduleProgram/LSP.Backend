using LSP.Core.Result;
using LSP.Entity.Concrete;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IClassroomTypeService
    {
        ServiceResult<bool> Add(ClassroomType ClassroomType);
        ServiceResult<bool> Update(ClassroomType ClassroomType);
        ServiceResult<bool> Delete(int id);
        ServiceResult<ClassroomType> GetById(int id);
        ServiceResult<List<ClassroomType>> GetList();
        ServiceResult<ClassroomType> Get(Expression<Func<ClassroomType, bool>> filter);
    }
}