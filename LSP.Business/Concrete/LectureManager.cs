using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using System.Linq.Expressions;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Department;

namespace LSP.Business.Concrete
{
    public class LectureManager : ILectureService
    {
        private readonly ILectureDal _lectureDal;
        private readonly IDepartmentService _departmentService;

        public LectureManager(ILectureDal lectureDal, IDepartmentService departmentService)
        {
            _lectureDal = lectureDal;
            _departmentService = departmentService;
        }

        public ServiceResult<bool> Add(AddLectureDto request)
        {
            if (string.IsNullOrEmpty(request.Name))
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.lecture_name_cant_empty,
                        Messages.lecture_name_cant_empty)
                };
            }

            var lectureWithSameName = _lectureDal.Get(x => x.Name == request.Name.Trim());
            if (lectureWithSameName is not null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.BadRequest,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.lecture_name_already_exist,
                        Messages.lecture_name_already_exist)
                };
            }

            var lecture = new Lecture
            {
                Name = request.Name
            };

            if (request.DepartmentId > 0)
            {
                var department = _departmentService.GetById((short)request.DepartmentId).Result;
                if (!department.Success)
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.NotFound,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.department_not_found,
                            Messages.department_not_found)
                    };
                }

                lecture.DepartmentId = request.DepartmentId;
            }

            _lectureDal.Add(lecture);

            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Update(UpdateLectureDto request)
        {
            var lectureFromDb = _lectureDal.Get(x => x.Id == request.Id);
            if (lectureFromDb is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.lecture_not_found,
                        Messages.lecture_not_found)
                };
            }

            if (!string.IsNullOrEmpty(request.Name))
            {
                if (request.Name == lectureFromDb.Name.Trim())
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.BadRequest,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.lecture_name_cant_same,
                            Messages.lecture_name_cant_same)
                    };
                }

                var lectureFromDbWithSameName = _lectureDal.Get(x => x.Name == request.Name.Trim());
                if (lectureFromDbWithSameName is not null)
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.BadRequest,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.lecture_name_already_exist,
                            Messages.lecture_name_already_exist)
                    };
                }

                lectureFromDb.Name = request.Name;
            }

            if (request.DepartmentId > 0)
            {
                var department = _departmentService.GetById((short)request.DepartmentId).Result;
                if (!department.Success)
                {
                    return new ServiceResult<bool>
                    {
                        HttpStatusCode = (short)HttpStatusCode.NotFound,
                        Result = new ErrorDataResult<bool>(false,
                            Messages.department_not_found,
                            Messages.department_not_found)
                    };
                }

                lectureFromDb.DepartmentId = request.DepartmentId;
            }

            lectureFromDb.UpdatedDate = DateTime.Now;
            _lectureDal.Update(lectureFromDb);

            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<bool> Delete(short id)
        {
            var lecture = _lectureDal.Get(x => x.Id == id);
            if (lecture is null)
            {
                return new ServiceResult<bool>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<bool>(false,
                        Messages.lecture_not_found,
                        Messages.lecture_not_found)
                };
            }

            _lectureDal.Delete(lecture);

            return new ServiceResult<bool>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<bool>(true,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<Lecture> Get(Expression<Func<Lecture, bool>> filter)
        {
            var result = _lectureDal.Get(filter);
            if (result is not null)
            {
                return new ServiceResult<Lecture>
                {
                    HttpStatusCode = (short)HttpStatusCode.OK,
                    Result = new SuccessDataResult<Lecture>(result,
                        Messages.success,
                        Messages.success_code)
                };
            }

            return new ServiceResult<Lecture>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<Lecture>(null,
                    Messages.lecture_not_found,
                    Messages.lecture_not_found)
            };
        }

        public ServiceResult<LectureDto> GetById(short id)
        {
            var lecture = _lectureDal.Get(x => x.Id == id);
            if (lecture is null)
            {
                return new ServiceResult<LectureDto>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new SuccessDataResult<LectureDto>(null,
                        Messages.lecture_not_found,
                        Messages.lecture_not_found)
                };
            }

            var lectureDto = new LectureDto()
            {
                Id = lecture!.Id,
                Name = lecture.Name,
                CreatedDate = lecture.CreatedDate,
                UpdatedDate = lecture.UpdatedDate,
            };

            if (lecture.DepartmentId > 0)
            {
                var department = _departmentService.GetById((short)lecture.DepartmentId).Result;
                if (!department.Success)
                {
                    return new ServiceResult<LectureDto>
                    {
                        HttpStatusCode = (short)HttpStatusCode.NotFound,
                        Result = new ErrorDataResult<LectureDto>(null,
                            Messages.department_not_found,
                            Messages.department_not_found)
                    };
                }

                lectureDto.DepartmentName = department.Data!.Name;
                lectureDto.FacultyName = department.Data.FacultyName;
            }

            return new ServiceResult<LectureDto>
            {
                HttpStatusCode = (short)HttpStatusCode.NotFound,
                Result = new ErrorDataResult<LectureDto>(lectureDto,
                    Messages.lecture_not_found,
                    Messages.lecture_not_found)
            };
        }

        public ServiceResult<List<LectureDto>> GetList()
        {
            var lecturesDto = new List<LectureDto>();

            var lectures = _lectureDal.GetList();

            if (lectures is null || lectures.Count is 0)
            {
                return new ServiceResult<List<LectureDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<List<LectureDto>>(lecturesDto,
                        Messages.lecture_not_found,
                        Messages.lecture_not_found)
                };
            }

            for (int i = 0; i < lectures.Count; i++)
            {
                var lectureDto = new LectureDto()
                {
                    Id = lectures[i].Id,
                    Name = lectures[i].Name,
                    DepartmentName = string.Empty,
                    FacultyName = string.Empty,
                    CreatedDate = lectures[i].CreatedDate,
                    UpdatedDate = lectures[i].UpdatedDate
                };

                if (lectures[i].DepartmentId > 0)
                {
                    var department = _departmentService.GetById((short)lectures[i].DepartmentId!).Result;
                    lectureDto.DepartmentName = department.Data!.Name;
                    lectureDto.FacultyName = department.Data.FacultyName;
                }

                lecturesDto.Add(lectureDto);
            }

            return new ServiceResult<List<LectureDto>>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<List<LectureDto>>(lecturesDto,
                    Messages.success,
                    Messages.success_code)
            };
        }
    }
}