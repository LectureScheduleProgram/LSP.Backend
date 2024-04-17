using System.ComponentModel.DataAnnotations;
using LSP.Core.Utilities.CustomAttributes;

namespace LSP.Entity.DTO.Authentication
{
    public class ForgetPasswordRequestDto
    {
        [Required] public string UserControlCode { get; set; }

        [Required] public string NewPassword { get; set; }

        [Required] public string CloneNewPassword { get; set; }

        [Required] public MfaTypeRequestDto MfaTypes { get; set; }
    }
}
