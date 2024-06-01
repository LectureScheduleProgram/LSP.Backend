using LSP.Core.Entities;

namespace LSP.Core.Pagination
{
    public class KeyValueParameterDto : IDto
    {
        public string Name { get; set; }
        public int[] Values { get; set; }
    }
}
