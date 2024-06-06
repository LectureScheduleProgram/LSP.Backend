using LSP.Core.Result;
using LSP.Entity.Concrete;
using LSP.Entity.DTO.ScheduleRecord;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Business.Abstract.Operations
{
    public interface IScheduleRecordOperationService
    {
        ServiceResult<List<ScheduleRecordDto>> GetList();
    }
}