using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.Enum.Classroom;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IClassroomCapacityService
    {
        ServiceResult<bool> Add(ClassroomCapacityEnum capacity);
        ServiceResult<bool> Update(ClassroomCapacity ClassroomCapacity);
        ServiceResult<bool> Delete(int id);
        ServiceResult<ClassroomCapacity> GetById(int id);
        ServiceResult<List<ClassroomCapacity>> GetList();
        ServiceResult<ClassroomCapacity> Get(Expression<Func<ClassroomCapacity, bool>> filter);
    }
}