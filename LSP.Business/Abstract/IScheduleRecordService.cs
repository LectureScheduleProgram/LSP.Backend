using LSP.Core.Result;
using LSP.Entity.Concrete;
using System.Linq.Expressions;

namespace LSP.Business.Abstract
{
    public interface IScheduleRecordService
    {
        ServiceResult<bool> Add(ScheduleRecord ScheduleRecord);
        ServiceResult<bool> Update(ScheduleRecord ScheduleRecord);
        ServiceResult<bool> Delete(int id);
        ServiceResult<ScheduleRecord> GetById(int id);
        ServiceResult<List<ScheduleRecord>> GetList();
        ServiceResult<ScheduleRecord> Get(Expression<Func<ScheduleRecord, bool>> filter);
    }
}