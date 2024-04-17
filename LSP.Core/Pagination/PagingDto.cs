using LSP.Core.Entities;

namespace LSP.Core.Pagination
{
    public class PagingDto : IDto
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public int TotalPage { get; set; }
        public int TotalSize { get; set; }
        public bool Prev { get; set; }
        public bool Next { get; set; }
    }
}
