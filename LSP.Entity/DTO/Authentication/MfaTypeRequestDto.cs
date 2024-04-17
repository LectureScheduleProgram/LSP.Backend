using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using LSP.Entity.Enums.Authentication;

namespace LSP.Entity.DTO.Authentication
{
    public class MfaTypeRequestDto
    {
        [Required]
        public MfaTypeEnum SecurityType { get; set; }
        [Required]
        [DefaultValue("100000")]
        public required string SecurityCode { get; set; }
    }
}
