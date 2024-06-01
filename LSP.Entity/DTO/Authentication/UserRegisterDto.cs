using LSP.Core.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LSP.Entity.DTO.Authentication
{
    public class RegisterDto : IDto
    {
        [Required]
        [DefaultValue("example@mail.com")]
        public string Email { get; set; }
        [Required]
        [DefaultValue("Password123?")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
