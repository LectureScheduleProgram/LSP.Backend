using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using LSP.Business.Abstract;
using LSP.Business.Constants;
using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.Dashboard;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Business.Concrete
{
    public class DashboardManager : IDashboardService
    {
        private readonly IScheduleRecordService _scheduleRecordService;
        private readonly IClassroomService _classroomService;
        private readonly IFacultyService _facultyService;
        private readonly IDepartmentService _departmentService;
        private readonly ILectureService _lectureService;

        public DashboardManager(IClassroomService classroomService, IScheduleRecordService scheduleRecordService, IFacultyService facultyService, IDepartmentService departmentService, ILectureService lectureService)
        {
            _classroomService = classroomService;
            _scheduleRecordService = scheduleRecordService;
            _facultyService = facultyService;
            _departmentService = departmentService;
            _lectureService = lectureService;
        }

        public ServiceResult<List<EntitiesDto>> StatisticsOfEntities()
        {
            var entities = new List<EntitiesDto>();

            var classrooms = _classroomService.GetList().Result;
            if (classrooms.Success)
                entities.Add(new EntitiesDto
                {
                    Name = nameof(Classroom),
                    Number = classrooms.Data!.Count
                });
            else
                entities.Add(new EntitiesDto { Name = nameof(Classroom) });

            var departments = _departmentService.GetList().Result;
            if (departments.Success)
                entities.Add(new EntitiesDto
                {
                    Name = nameof(Department),
                    Number = departments.Data!.Count
                });
            else
                entities.Add(new EntitiesDto { Name = nameof(Department) });

            var faculties = _facultyService.GetList().Result;
            if (faculties.Success)
                entities.Add(new EntitiesDto
                {
                    Name = nameof(Faculty),
                    Number = faculties.Data!.Count
                });
            else
                entities.Add(new EntitiesDto { Name = nameof(Faculty) });

            var lectures = _lectureService.GetList().Result;
            if (lectures.Success)
                entities.Add(new EntitiesDto
                {
                    Name = nameof(Lecture),
                    Number = lectures.Data!.Count
                });
            else
                entities.Add(new EntitiesDto { Name = nameof(Lecture) });

            var scheduleRecords = _scheduleRecordService.GetList().Result;
            if (scheduleRecords.Success)
                entities.Add(new EntitiesDto
                {
                    Name = nameof(ScheduleRecord),
                    Number = scheduleRecords.Data!.Count
                });
            else
                entities.Add(new EntitiesDto { Name = nameof(ScheduleRecord) });

            return new ServiceResult<List<EntitiesDto>>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<List<EntitiesDto>>(entities,
                    Messages.success,
                    Messages.success_code)
            };
        }

        public ServiceResult<OpenCloseClassResponseDto> AvailabilityOfClasses(OpenCloseClassRequestDto request)
        {
            // if (request.StartHour == request.EndHour)
            //     return new ServiceResult<OpenCloseClassResponseDto>
            //     {
            //         HttpStatusCode = (short)HttpStatusCode.NotFound,
            //         Result = new ErrorDataResult<OpenCloseClassResponseDto>(null,
            //             Messages.same_start_end_hour,
            //             Messages.same_start_end_hour)
            //     };

            // if (request.StartHour > request.EndHour)
            //     return new ServiceResult<OpenCloseClassResponseDto>
            //     {
            //         HttpStatusCode = (short)HttpStatusCode.NotFound,
            //         Result = new ErrorDataResult<OpenCloseClassResponseDto>(
            //             null,
            //             Messages.start_hour_must_smaller,
            //             Messages.start_hour_must_smaller)
            //     };

            // var classrooms = _classroomService.GetList().Result;

            // if (!classrooms.Success)
            //     return new ServiceResult<OpenCloseClassResponseDto>
            //     {
            //         HttpStatusCode = (short)HttpStatusCode.NotFound,
            //         Result = new ErrorDataResult<OpenCloseClassResponseDto>(null,
            //             Messages.classroom_not_found,
            //             Messages.classroom_not_found)
            //     };

            // var scheduleRecords = _scheduleRecordService.GetListByClassroomIds(classrooms.Data!.Select(c => c.Id).ToList()).Result;
            // if (!scheduleRecords.Success)
            // {
            //     var openCloseClassDto = new OpenCloseClassResponseDto
            //     {
            //         NumberOfAvailables = classrooms.Data!.Count,
            //         NumberOfUnavailables = 0
            //     };

            //     return new ServiceResult<OpenCloseClassResponseDto>
            //     {
            //         HttpStatusCode = (short)HttpStatusCode.OK,
            //         Result = new SuccessDataResult<OpenCloseClassResponseDto>(
            //             openCloseClassDto,
            //             Messages.success,
            //             Messages.success)
            //     };
            // }
            // else
            // {
            //     var unavailableClassrooms =

            //     var openCloseClassDto = new OpenCloseClassResponseDto
            //     {
            //         NumberOfAvailables = classrooms.Data!.Count - scheduleRecords.Result.Data!.Count,
            //         NumberOfUnavailables = scheduleRecords.Result.Data!.Count
            //     };

            //     return new ServiceResult<OpenCloseClassResponseDto>
            //     {
            //         HttpStatusCode = (short)HttpStatusCode.OK,
            //         Result = new SuccessDataResult<OpenCloseClassResponseDto>(
            //             openCloseClassDto,
            //             Messages.success,
            //             Messages.success)
            //     };
            // }

            return new ServiceResult<OpenCloseClassResponseDto>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<OpenCloseClassResponseDto>(
                    new OpenCloseClassResponseDto()
                    {
                        NumberOfAvailables = 24,
                        NumberOfUnavailables = 25
                    },
                    Messages.success,
                    Messages.success_code)
            };
        }

        public class OpenCloseClassRequestDto
        {
            [DefaultValue(DaysEnum.Monday)]
            [Required]
            public required DaysEnum Day { get; set; }

            [Range(1, 24)]
            [DefaultValue((byte)1)]
            public required byte StartHour { get; set; }

            [DefaultValue((byte)1)]
            [Range(1, 24)]
            public required byte EndHour { get; set; }
        }
    }
}