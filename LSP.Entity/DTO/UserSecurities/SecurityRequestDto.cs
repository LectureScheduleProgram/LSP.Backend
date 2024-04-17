using System.ComponentModel.DataAnnotations;
using LSP.Core.Entities;
using LSP.Core.Utilities.CustomAttributes;
using LSP.Entity.DTO.Authentication;

namespace LSP.Entity.DTO.UserSecurities
{
    public class SecurityRequestDto : IDto
    {
        [Required]
        [NotEmptyList]
        public List<MfaTypeRequestDto> MfaTypes { get; set; }
    }
}
