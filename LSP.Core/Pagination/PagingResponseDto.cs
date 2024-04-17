using LSP.Core.Entities;

namespace LSP.Core.Pagination
{
    public class PagingResponseDto<T> : IDto
        where T : class, new()
    {
        public List<T> Data { get; set; }
        public PagingDto Pagination { get; set; }
    }
}
