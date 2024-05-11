using LSP.Core;
using LSP.Entity.Abstract.Common;

namespace LSP.Entity.Concrete
{
    public class ClassroomType : BaseEntity, IEntity
    {
        public byte Id { get; set; }
        public string Name { get; set; } = "UNKNOWN";
    }
}