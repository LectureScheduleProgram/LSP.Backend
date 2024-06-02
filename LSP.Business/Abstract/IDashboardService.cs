using LSP.Core.Result;
using LSP.Entity.DTO.Dashboard;
using static LSP.Business.Concrete.DashboardManager;

namespace LSP.Business.Abstract
{
    public interface IDashboardService
    {
        ServiceResult<List<EntitiesDto>> StatisticsOfEntities();
        ServiceResult<OpenCloseClassResponseDto> AvailabilityOfClasses(OpenCloseClassRequestDto request);
    }
}