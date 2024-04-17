using System.ComponentModel.DataAnnotations;
using LSP.Entity.Enums.Authentication;

namespace LSP.Entity.DTO.Authentication
{
    public class MfaTypeResponseDto
    {
        [Required]
        public MfaTypeEnum SecurityType { get; set; }
    }
}
