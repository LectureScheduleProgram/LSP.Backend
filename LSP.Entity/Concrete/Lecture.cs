using LSP.Core;
using LSP.Entity.Abstract.Common;

namespace LSP.Entity.Concrete
{
    public class Lecture : BaseEntity, IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // public Faculty Faculty { get; set; }
        // public short FacultyId { get; set; }

        // public Department Department { get; set; }
        // public short DepartmentId { get; set; }
    }
}