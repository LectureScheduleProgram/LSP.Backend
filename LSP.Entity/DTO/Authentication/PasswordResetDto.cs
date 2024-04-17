using LSP.Core.Entities;
using System.ComponentModel.DataAnnotations;
using LSP.Core.Utilities.CustomAttributes;

namespace LSP.Entity.DTO.Authentication
{
    public class PasswordResetDto : IDto
    {
        [Required] public string OldPassword { get; set; }
        [Required] public string NewPassword { get; set; }
        [Required] public string CloneNewPassword { get; set; }
        [Required][NotEmptyList] public List<MfaTypeRequestDto> MfaTypes { get; set; }
    }
}