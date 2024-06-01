using LSP.Core;
using LSP.Entity.Abstract.Common;

namespace LSP.Entity.Concrete
{
    public class Department : BaseEntity, IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public short FacultyId { get; set; }
    }
}