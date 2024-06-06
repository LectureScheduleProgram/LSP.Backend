using LSP.Business.Abstract;
using LSP.Core.Result;
using LSP.Dal.Abstract;
using LSP.Business.Constants;
using System.Net;
using LSP.Entity.DTO.ScheduleRecord;
using LSP.Business.Abstract.Operations;

namespace LSP.Business.Concrete.Operations
{
    public class ScheduleRecordOperationManager : IScheduleRecordOperationService
    {
        private readonly IScheduleRecordDal _scheduleRecordDal;
        private readonly IClassroomService _classroomService;
        private readonly ILectureService _lectureService;

        public ScheduleRecordOperationManager(IScheduleRecordDal scheduleRecordDal, IClassroomService classroomService, ILectureService lectureService)
        {
            _scheduleRecordDal = scheduleRecordDal;
            _classroomService = classroomService;
            _lectureService = lectureService;
        }

        public ServiceResult<List<ScheduleRecordDto>> GetList()
        {
            var scheduleRecordDtoList = new List<ScheduleRecordDto>();

            var scheduleRecords = _scheduleRecordDal.GetList();
            if (scheduleRecords is null || !scheduleRecords.Any())
            {
                return new ServiceResult<List<ScheduleRecordDto>>
                {
                    HttpStatusCode = (short)HttpStatusCode.NotFound,
                    Result = new ErrorDataResult<List<ScheduleRecordDto>>(
                        scheduleRecordDtoList,
                        Messages.success,
                        Messages.success_code)
                };
            }

            for (int i = 0; i < scheduleRecords!.Count; i++)
            {
                var scheduleRecordDto = new ScheduleRecordDto
                {
                    Id = scheduleRecords[i].Id,
                    ClassroomName = _classroomService.GetById(scheduleRecords[i].ClassroomId).Result.Data!.Name,
                    LectureName = _lectureService.GetById(scheduleRecords[i].LectureId).Result.Data!.Name,
                    Day = scheduleRecords[i].Day,
                    StartHour = scheduleRecords[i].StartHour,
                    EndHour = scheduleRecords[i].EndHour
                };

                scheduleRecordDtoList.Add(scheduleRecordDto);
            }

            return new ServiceResult<List<ScheduleRecordDto>>
            {
                HttpStatusCode = (short)HttpStatusCode.OK,
                Result = new SuccessDataResult<List<ScheduleRecordDto>>(scheduleRecordDtoList)
            };
        }
    }
}