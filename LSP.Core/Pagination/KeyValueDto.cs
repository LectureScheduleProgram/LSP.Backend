using LSP.Core.Entities;

namespace LSP.Core.Pagination
{
    public class KeyValueDto : IDto
    {
        public string Key { get; set; }
        public object Value { get; set; }
        public string Parameter { get; set; }
    }
}
