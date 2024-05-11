using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Lecture;

namespace LSP.Business.Concrete
{
    public class ClassroomManager : IClassroomService
    {
        private readonly IClassroomDal _classroomsDal;

        public ClassroomManager(IClassroomDal ClassroomsDal)
        {
            _classroomsDal = ClassroomsDal;
        }
        #region CRUD
        public ServiceResult<bool> Add(Classroom Classroom)
        {
            _classroomsDal.Add(Classroom);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(Classroom Classroom)
        {
            var getClassroom = _classroomsDal.Get(x => x.Id == Classroom.Id);
            if (getClassroom is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroom_not_found,
                        Messages.classroom_not_found)
                };
            }

            _classroomsDal.Update(Classroom);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Delete(int id)
        {
            var Classroom = _classroomsDal.Get(x => x.Id == id);
            if (Classroom is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroom_not_found,
                        Messages.classroom_not_found)
                };
            }

            _classroomsDal.Delete(Classroom);
            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<Classroom> Get(Expression<Func<Classroom, bool>> filter)
        {
            var result = _classroomsDal.Get(filter);
            if (result is not null)
            {
                return new ServiceResult<Classroom>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<Classroom>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<Classroom>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<Classroom>(null,
                    Messages.classroom_not_found,
                    Messages.classroom_not_found)
            };
        }

        public ServiceResult<Classroom> GetById(int id)
        {
            var Classroom = _classroomsDal.Get(x => x.Id == id);
            if (Classroom is not null)
            {
                return new ServiceResult<Classroom>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<Classroom>(Classroom,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<Classroom>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<Classroom>(new Classroom(),
                    Messages.classroom_not_found,
                    Messages.classroom_not_found)
            };
        }

        public ServiceResult<List<Classroom>> GetList()
        {
            var Classrooms = _classroomsDal.GetList();
            if (Classrooms is not null)
            {
                return new ServiceResult<List<Classroom>>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<List<Classroom>>(Classrooms.ToList(),
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<List<Classroom>>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<List<Classroom>>(new List<Classroom>(),
                    Messages.classroom_not_found,
                    Messages.classroom_not_found)
            };
        }
        #endregion

        public ServiceResult<List<GetAvailableClassroomListResponseDto>> GetAvailableClassroomList(GetAvailableClassroomListRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}