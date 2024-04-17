using LSP.Core.Entities;

namespace LSP.Core.Pagination
{
    public class PagingRequestDto : IDto
    {
        private int page;
        private int size;
        public int Page { get { return page; } set { page = value <= 0 ? 1 : value; } }
        public int Size { get { return size; } set { size = value <= 0 ? 10 : value; } }
    }
}
