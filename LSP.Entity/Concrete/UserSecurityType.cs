using LSP.Core;

namespace LSP.Entity.Concrete
{
    public class UserSecurityType : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string SecurityType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public byte Status { get; set; }
    }
}