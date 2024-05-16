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
        private readonly IScheduleRecordService _scheduleRecordService;
        private readonly IClassroomCapacityService _classroomCapacityService;
        private readonly IClassroomTypeService _classroomTypeService;

        public ClassroomManager(IClassroomDal classroomsDal, IScheduleRecordService scheduleRecordService, IClassroomCapacityService classroomCapacityService, IClassroomTypeService classroomTypeService)
        {
            _classroomsDal = classroomsDal;
            _scheduleRecordService = scheduleRecordService;
            _classroomCapacityService = classroomCapacityService;
            _classroomTypeService = classroomTypeService;
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

        public ServiceResult<GetAvailableClassroomResponseDto> GetAvailableClassroom(GetAvailableClassroomRequestDto request)
        {
            var classrooms = _classroomsDal.GetList(c =>
                c.ClassroomCapacityId == request.ClassroomCapacityId &&
                c.ClassroomTypeId == request.ClassroomTypeId)
                .ToList();

            if (classrooms is null || classrooms.Count is 0)
            {
                return new ServiceResult<GetAvailableClassroomResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<GetAvailableClassroomResponseDto>(null,
                        Messages.classroom_not_found,
                        Messages.classroom_not_found)
                };
            }

            var scheduleRecords = _scheduleRecordService.GetListByClassroomIds(classrooms.Select(c => c.Id).ToList());
            if (!scheduleRecords.Result.Success)
            {
                var availableClassroom = GetRandomClassroom(classrooms);

                return new ServiceResult<GetAvailableClassroomResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new SuccessDataResult<GetAvailableClassroomResponseDto>(
                        availableClassroom,
                        Messages.success,
                        Messages.success)
                };
            }
            else
            {
                for (int i = 0; i < classrooms.Count; i++)
                {
                    if (scheduleRecords.Result.Data!.Exists(sr =>
                        sr.ClassroomId == classrooms[i].Id &&
                        sr.Day == request.Day &&
                        (
                            (sr.StartHour == request.StartHour && sr.EndHour == request.EndHour) ||
                            (sr.StartHour < request.StartHour && sr.EndHour > request.EndHour) ||
                            (sr.StartHour < request.StartHour && sr.EndHour < request.EndHour) ||
                            (sr.StartHour < request.StartHour && sr.EndHour < request.EndHour) ||
                            (sr.StartHour < request.StartHour && sr.EndHour > request.EndHour)
                        )
                    )) classrooms.Remove(classrooms[i]);
                }
            }

            if (classrooms.Count > 0)
            {
                var availableClassroom = GetRandomClassroom(classrooms);

                return new ServiceResult<GetAvailableClassroomResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new SuccessDataResult<GetAvailableClassroomResponseDto>(
                        availableClassroom,
                        Messages.success,
                        Messages.success)
                };
            }

            return new ServiceResult<GetAvailableClassroomResponseDto>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<GetAvailableClassroomResponseDto>(
                    null,
                    Messages.classroom_not_found,
                    Messages.classroom_not_found)
            };
        }

        public ServiceResult<List<GetAvailableClassroomResponseDto>> GetAvailableClassroomList(GetAvailableClassroomListRequestDto request)
        {
            var classrooms = _classroomsDal.GetList(c =>
                c.ClassroomCapacityId == request.ClassroomCapacityId &&
                c.ClassroomTypeId == request.ClassroomTypeId)
                .ToList();

            if (classrooms is null || classrooms.Count is 0)
            {
                return new ServiceResult<List<GetAvailableClassroomResponseDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<List<GetAvailableClassroomResponseDto>>(null,
                        Messages.classroom_not_found,
                        Messages.classroom_not_found)
                };
            }

            var scheduleRecords = _scheduleRecordService.GetListByClassroomIds(classrooms.Select(c => c.Id).ToList());
            if (!scheduleRecords.Result.Success)
            {
                var availableClassrooms = classrooms.Select(c => new GetAvailableClassroomResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    //TODO: InMemoryCaching needs to be applied for classroomType and classroomCapacity to increase performance 
                    ClassroomType = _classroomTypeService.GetById(c.ClassroomTypeId).Result.Data!.Name,
                    ClassroomCapacity = _classroomCapacityService.GetById(c.ClassroomCapacityId).Result.Data!.Capacity
                }).ToList();

                return new ServiceResult<List<GetAvailableClassroomResponseDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new SuccessDataResult<List<GetAvailableClassroomResponseDto>>(
                        availableClassrooms,
                        Messages.success,
                        Messages.success)
                };
            }
            else
            {
                for (int i = 0; i < classrooms.Count; i++)
                {
                    if (scheduleRecords.Result.Data!.Exists(sr =>
                        sr.ClassroomId == classrooms[i].Id &&
                        sr.Day == request.Day &&
                        (
                            (sr.StartHour == request.StartHour && sr.EndHour == request.EndHour) ||
                            (sr.StartHour < request.StartHour && sr.EndHour > request.EndHour) ||
                            (sr.StartHour < request.StartHour && sr.EndHour < request.EndHour) ||
                            (sr.StartHour < request.StartHour && sr.EndHour < request.EndHour) ||
                            (sr.StartHour < request.StartHour && sr.EndHour > request.EndHour)
                        )
                    )) classrooms.Remove(classrooms[i]);
                }
            }

            if (classrooms.Count > 0)
            {
                var availableClassrooms = classrooms.Select(c => new GetAvailableClassroomResponseDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ClassroomType = _classroomTypeService.GetById(c.ClassroomTypeId).Result.Data!.Name,
                    ClassroomCapacity = _classroomCapacityService.GetById(c.ClassroomCapacityId).Result.Data!.Capacity
                }).ToList();

                return new ServiceResult<List<GetAvailableClassroomResponseDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new SuccessDataResult<List<GetAvailableClassroomResponseDto>>(
                        availableClassrooms,
                        Messages.success,
                        Messages.success)
                };
            }

            return new ServiceResult<List<GetAvailableClassroomResponseDto>>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<List<GetAvailableClassroomResponseDto>>(
                    null,
                    Messages.classroom_not_found,
                    Messages.classroom_not_found)
            };
        }

        private GetAvailableClassroomResponseDto GetRandomClassroom(List<Classroom> classrooms)
        {
            var classroom = classrooms[new Random().Next(0, classrooms.Count - 1)];

            return new GetAvailableClassroomResponseDto()
            {
                Id = classroom.Id,
                Name = classroom.Name,
                ClassroomType = _classroomTypeService.GetById(classroom.ClassroomTypeId).Result.Data!.Name,
                ClassroomCapacity = _classroomCapacityService.GetById(classroom.ClassroomCapacityId).Result.Data!.Capacity,
            };
        }
    }
}