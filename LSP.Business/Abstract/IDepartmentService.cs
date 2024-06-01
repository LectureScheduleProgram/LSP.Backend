using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Department;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IDepartmentService
    {
        ServiceResult<bool> Add(AddDepartmentDto request);
        ServiceResult<bool> Update(UpdateDepartmentDto request);
        ServiceResult<bool> Delete(short id);
        ServiceResult<DepartmentDto> GetById(short id);
        ServiceResult<List<DepartmentDto>> GetList();
        ServiceResult<Department> Get(Expression<Func<Department, bool>> filter);
    }
}