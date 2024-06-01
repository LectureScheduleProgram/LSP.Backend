using LSP.Core.Entities;

namespace LSP.Core.Pagination.Procedure
{
    public class PagingProcedureResponseDto<T> : IDto
        where T : class, IDto, new()
    {
        public List<T> Data { get; set; }
        public PagingDto Page { get; set; }
    }
}
