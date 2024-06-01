using LSP.Core.Entities;
using System.Linq.Expressions;

namespace LSP.Core.Pagination.Procedure
{
    public interface IPagingProcedure<TEntity> where TEntity : class, IDto, new()
    {
        PagingProcedureResponseDto<TEntity> GetPagingList(PagingRequestDto pagingRequestDto,
            Expression<Func<TEntity, object>> orderBy = null, Expression<Func<TEntity, object>> thenOrderBy = null, bool isDesc = false, List<string> searchTypes = null, IDictionary<string, string> stringParameters = null, IDictionary<string, int?> intParameters = null, List<KeyValueDto> dateTimeParameters = null, IQueryable<TEntity> list = null, string search = null);
    }
}
