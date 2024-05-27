using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Lecture;
using LSP.Entity.DTO.ClassroomCapacity;
using LSP.Entity.DTO.Classroom;

namespace LSP.Business.Concrete
{
    public class ClassroomManager : IClassroomService
    {
        private readonly IClassroomDal _classroomsDal;
        private readonly IScheduleRecordService _scheduleRecordService;
        private readonly IClassroomCapacityService _classroomCapacityService;
        private readonly IClassroomTypeService _classroomTypeService;

        public ClassroomManager(IClassroomDal classroomsDal, IClassroomCapacityService classroomCapacityService, IClassroomTypeService classroomTypeService, IScheduleRecordService scheduleRecordService)
        {
            _classroomsDal = classroomsDal;
            _classroomCapacityService = classroomCapacityService;
            _classroomTypeService = classroomTypeService;
            _scheduleRecordService = scheduleRecordService;
        }
        #region CRUD
        public ServiceResult<bool> Add(AddClassroomDto classroom)
        {
            var classroomFromDb = _classroomsDal.Get(x => x.Name == classroom.Name.Trim());
            if (classroomFromDb is not null)
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroom_already_exists,
                        Messages.classroom_already_exists)
                };

            _classroomsDal.Add(new Classroom()
            {
                Name = classroom.Name,
                ClassroomCapacityId = classroom.ClassroomCapacityId,
                ClassroomTypeId = classroom.ClassroomTypeId
            });

            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(UpdateClassroomDto request)
        {
            var classroomFromDb = _classroomsDal.Get(x => x.Id == request.Id);
            if (classroomFromDb is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroom_not_found,
                        Messages.classroom_not_found)
                };
            }

            if (classroomFromDb.Name.Equals(request.Name.Trim()))
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                            Messages.classroom_name_same,
                            Messages.classroom_name_same)
                };

            var classroomExists = _classroomsDal.Get(c => c.Name == request.Name.Trim());
            if (classroomExists.Id != classroomFromDb.Id)
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.classroom_already_exists,
                        Messages.classroom_already_exists)
                };

            classroomFromDb.Name = request.Name;
            classroomFromDb.UpdatedDate = DateTime.Now;

            _classroomsDal.Update(classroomFromDb);
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
            if (request.StartHour == request.EndHour)
                return new ServiceResult<GetAvailableClassroomResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<GetAvailableClassroomResponseDto>(null,
                        Messages.same_start_end_hour,
                        Messages.same_start_end_hour)
                };

            if (request.StartHour > request.EndHour)
                return new ServiceResult<GetAvailableClassroomResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<GetAvailableClassroomResponseDto>(
                        null,
                        Messages.start_hour_must_smaller,
                        Messages.start_hour_must_smaller)
                };

            var classrooms = _classroomsDal.GetList(c =>
                c.ClassroomCapacityId == request.ClassroomCapacityId &&
                c.ClassroomTypeId == request.ClassroomTypeId)
                .ToList();

            if (classrooms is null || classrooms.Count is 0)
                return new ServiceResult<GetAvailableClassroomResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new ErrorDataResult<GetAvailableClassroomResponseDto>(null,
                        Messages.classroom_not_found,
                        Messages.classroom_not_found)
                };

            var scheduleRecords = _scheduleRecordService.GetListByClassroomIds(classrooms.Select(c => c.Id).ToList());
            if (!scheduleRecords.Result.Success)
            {
                var availableClassroom = GetRandomClassroom(classrooms);

                return new ServiceResult<GetAvailableClassroomResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<GetAvailableClassroomResponseDto>(
                        availableClassroom,
                        Messages.success,
                        Messages.success)
                };
            }
            else
                EliminateMatchedClasses(request, classrooms, scheduleRecords.Result.Data!);

            if (classrooms.Count > 0)
            {
                var availableClassroom = GetRandomClassroom(classrooms);

                return new ServiceResult<GetAvailableClassroomResponseDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
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

        public ServiceResult<List<GetAvailableClassroomResponseDto>> GetAvailableClassroomList(GetAvailableClassroomRequestDto request)
        {
            if (request.StartHour == request.EndHour)
                return new ServiceResult<List<GetAvailableClassroomResponseDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<List<GetAvailableClassroomResponseDto>>(
                        null,
                        Messages.same_start_end_hour,
                        Messages.same_start_end_hour)
                };

            if (request.StartHour > request.EndHour)
                return new ServiceResult<List<GetAvailableClassroomResponseDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<List<GetAvailableClassroomResponseDto>>(
                        null,
                        Messages.start_hour_must_smaller,
                        Messages.start_hour_must_smaller)
                };

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
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<List<GetAvailableClassroomResponseDto>>(
                        availableClassrooms,
                        Messages.success,
                        Messages.success)
                };
            }
            else
                EliminateMatchedClasses(request, classrooms, scheduleRecords.Result.Data!);

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
                    HttpStatusCode = (short)HttpStatusCode.OK,
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

        private void EliminateMatchedClasses(GetAvailableClassroomRequestDto request, List<Classroom> classrooms, List<ScheduleRecord> scheduleRecords)
        {
            for (int i = 0; i < scheduleRecords.Count; i++)
            {
                if (_scheduleRecordService.TimeControl(scheduleRecords, request.Day, request.StartHour, request.EndHour))
                    classrooms.Remove(classrooms[i]);
            }
        }

        private GetAvailableClassroomResponseDto GetRandomClassroom(List<Classroom> classrooms)
        {
            var randomIdIndex = new Random().Next(0, classrooms.Count);
            var classroom = classrooms[randomIdIndex];

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