using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Lecture;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IClassroomService
    {
        #region CRUD
        ServiceResult<bool> Add(Classroom Classroom);
        ServiceResult<bool> Update(Classroom Classroom);
        ServiceResult<bool> Delete(int id);
        ServiceResult<Classroom> GetById(int id);
        ServiceResult<List<Classroom>> GetList();
        ServiceResult<Classroom> Get(Expression<Func<Classroom, bool>> filter);
        #endregion

        ServiceResult<List<GetAvailableClassroomListResponseDto>> GetAvailableClassroomList(GetAvailableClassroomListRequestDto request);
    }
}