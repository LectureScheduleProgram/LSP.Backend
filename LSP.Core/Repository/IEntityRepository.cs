using LSP.Core.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LSP.Core.Repository
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        T Get(Expression<Func<T, bool>> filter = null);
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        void Add(T entity);
        void Delete(T entity);
        void Update(T entity);

        PagingResponseDto<T> GetPagingList(PagingRequestDto pagingRequestDto,
            Expression<Func<T, bool>> filters = null, Expression<Func<T, object>> orderby = null);
        PagingResponseDto<T> GetPagingList(PagingRequestDto pagingRequestDto,
            Expression<Func<T, object>> orderBy = null, Expression<Func<T, object>> thenOrderBy = null, bool isDesc = false, List<string> searchTypes = null, IDictionary<string, string> stringParameters = null, IDictionary<string, int?> intParameters = null, IDictionary<string, bool?> boolParameters = null, IDictionary<string, byte?> byteParameters = null, List<KeyValueDto> dateTimeParameters = null, List<KeyValueParameterDto> parameters = null, string search = null);
    }
}
