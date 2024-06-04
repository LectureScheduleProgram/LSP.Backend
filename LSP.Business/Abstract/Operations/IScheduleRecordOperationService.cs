using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.ScheduleRecord;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Business.Abstract.Operations
{
    public interface IScheduleRecordOperationService
    {
        ServiceResult<bool> Add(AddScheduleRecordDto request);
        ServiceResult<List<ScheduleRecordDto>> GetList();
        ServiceResult<List<ScheduleRecord>> GetListByClassroomIds(List<short> classroomId);
        bool TimeControl(List<ScheduleRecord> list, DaysEnum day, byte startHour, byte endHour);
    }
}