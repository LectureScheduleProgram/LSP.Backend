using LSP.Core;
using LSP.Entity.Abstract.Common;

namespace LSP.Entity.Concrete
{
    public class Classroom : BaseEntity, IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; } = Guid.NewGuid().ToString();
        public byte ClassroomTypeId { get; set; }
        public byte ClassroomCapacityId { get; set; }

        // public Block Block { get; set; } A Block, B Block...
        // public short BlockId { get; set; }
    }
}