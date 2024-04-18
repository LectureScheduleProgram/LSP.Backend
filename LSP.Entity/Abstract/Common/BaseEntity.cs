using LSP.Entity.Enum.Common;

namespace LSP.Entity.Abstract.Common
{
    public abstract class BaseEntity
    {
        public StatusEnum Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}