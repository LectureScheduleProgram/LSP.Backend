using LSP.Core;
using System;

namespace LSP.Entity.Concrete
{
    public class PasswordHistory : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}