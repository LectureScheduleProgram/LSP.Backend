using LSP.Core;
using LSP.Entity.Abstract.Common;

namespace LSP.Entity.Concrete
{
    public class Faculty : BaseEntity, IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}