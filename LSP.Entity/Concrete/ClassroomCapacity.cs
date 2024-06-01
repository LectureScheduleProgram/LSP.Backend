using LSP.Core;
using LSP.Entity.Abstract.Common;

namespace LSP.Entity.Concrete
{
    public class ClassroomCapacity : BaseEntity, IEntity
    {
        public short Id { get; set; }
        public short Capacity { get; set; }
    }
}