using System.ComponentModel.DataAnnotations;
using LSP.Core.Entities;
using LSP.Core.Utilities.CustomAttributes;

namespace LSP.Entity.DTO.Authentication
{
    public class SecurityWithUserControlRequestDto : IDto
    {
        [Required]
        public string UserControlCode { get; set; }
        [Required]
        [NotEmptyList]
        public List<MfaTypeRequestDto> MfaTypes { get; set; }
    }
}