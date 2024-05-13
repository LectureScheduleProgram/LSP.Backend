using LSP.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LSP.Entity.DTO
{
    public class LoginDto : IDto
    {
        [DefaultValue("example@mail.com")]
        public required string Email { get; set; }

        [DefaultValue("Password123?")]
        public required string Password { get; set; }
    }
}