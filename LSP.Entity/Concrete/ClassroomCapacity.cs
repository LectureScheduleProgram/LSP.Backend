using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LSP.Core;
using LSP.Entity.Abstract.Common;
using Microsoft.EntityFrameworkCore;

namespace LSP.Entity.Concrete
{
    public class ClassroomCapacity : BaseEntity, IEntity
    {
        public short Id { get; set; }
        public short Capacity { get; set; }
    }
}