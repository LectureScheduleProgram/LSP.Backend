using LSP.Core;
using System;

namespace LSP.Entity.Concrete
{
    public class SecurityHistory : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int UserSecurityTypeId { get; set; }
        public string? SecurityCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? Seperation { get; set; }
        public DateTime? ExpireDate { get; set; }
        public byte Status { get; set; }
    }
}