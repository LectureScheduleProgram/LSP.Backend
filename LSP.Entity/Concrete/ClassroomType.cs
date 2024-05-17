using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LSP.Core;
using LSP.Entity.Abstract.Common;

namespace LSP.Entity.Concrete
{
    public class ClassroomType : BaseEntity, IEntity
    {
        public short Id { get; set; }
        public string Name { get; set; } = "UNKNOWN";
    }
}