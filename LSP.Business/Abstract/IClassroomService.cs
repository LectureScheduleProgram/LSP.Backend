using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.ClassroomCapacity;
using LSP.Entity.DTO.Lecture;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IClassroomService
    {
        #region CRUD
        ServiceResult<bool> Add(AddClassroomDto Classroom);
        ServiceResult<bool> Update(Classroom Classroom);
        ServiceResult<bool> Delete(int id);
        ServiceResult<Classroom> GetById(int id);
        ServiceResult<List<Classroom>> GetList();
        ServiceResult<Classroom> Get(Expression<Func<Classroom, bool>> filter);
        #endregion

        ServiceResult<List<GetAvailableClassroomResponseDto>> GetAvailableClassroomList(GetAvailableClassroomListRequestDto request);
        ServiceResult<GetAvailableClassroomResponseDto> GetAvailableClassroom(GetAvailableClassroomRequestDto request);
    }
}