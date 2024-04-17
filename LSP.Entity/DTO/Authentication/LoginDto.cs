using LSP.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LSP.Entity.DTO
{
    public class LoginDto : IDto
    {
        [Required]
        [DefaultValue("example@mail.com")]
        public string Email { get; set; }

        [Required]
        [DefaultValue("Password123?")]
        public string Password { get; set; }
    }
}