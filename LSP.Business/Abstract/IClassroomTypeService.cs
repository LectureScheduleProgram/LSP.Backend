using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.Enum.Classroom;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IClassroomTypeService
    {
        ServiceResult<bool> Add(ClassroomTypeEnum type);
        ServiceResult<bool> Update(ClassroomType ClassroomType);
        ServiceResult<bool> Delete(int id);
        ServiceResult<ClassroomType> GetById(int id);
        ServiceResult<List<ClassroomType>> GetList();
        ServiceResult<ClassroomType> Get(Expression<Func<ClassroomType, bool>> filter);
    }
}