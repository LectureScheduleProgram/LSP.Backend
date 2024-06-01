using LSP.Core;

namespace LSP.Entity.Concrete
{
    public class UserStatusHistory : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public short StatusId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
    }
}