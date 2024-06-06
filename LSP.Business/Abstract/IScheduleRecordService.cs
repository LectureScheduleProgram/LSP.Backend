using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.ScheduleRecord;
using LSP.Entity.Enum.ScheduleRecord;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IScheduleRecordService
    {
        ServiceResult<bool> Add(AddScheduleRecordDto request);
        ServiceResult<bool> Update(ScheduleRecord ScheduleRecord);
        ServiceResult<bool> Delete(int id);
        ServiceResult<ScheduleRecord> GetById(int id);
        ServiceResult<List<ScheduleRecord>> GetList();
        ServiceResult<List<ScheduleRecord>> GetListByClassroomIds(List<short> classroomId);
        // ServiceResult<List<GetScheduleRecordListByStatusDto>> GetListByStatus(ScheduleRecordStatusEnum status);
        ServiceResult<ScheduleRecord> Get(Expression<Func<ScheduleRecord, bool>> filter);
        bool ScheduleAvailabilityControl(short classroomId, DaysEnum day, byte startHour, byte endHour);
    }
}