using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Department;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface ILectureService
    {
        ServiceResult<bool> Add(AddLectureDto request);
        ServiceResult<bool> Update(UpdateLectureDto request);
        ServiceResult<bool> Delete(short id);
        ServiceResult<LectureDto> GetById(short id);
        ServiceResult<List<LectureDto>> GetList();
        ServiceResult<Lecture> Get(Expression<Func<Lecture, bool>> filter);
    }
}