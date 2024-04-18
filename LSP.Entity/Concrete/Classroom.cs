using LSP.Core;
using LSP.Entity.Abstract.Common;
using LSP.Entity.Enum.Classroom;

namespace LSP.Entity.Concrete
{
    public class Classroom : BaseEntity, IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public ClassroomTypeEnum ClassroomType { get; set; } // It can be a sepearted entity
        public ClassroomCapacityEnum ClassroomCapacity { get; set; } // It can be a sepearted entity

        // public Block Block { get; set; } A Block, B Block...
        // public short BlockId { get; set; }
    }
}