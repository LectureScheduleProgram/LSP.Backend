using LSP.Core.Entities;
using System.ComponentModel.DataAnnotations;
using LSP.Entity.Enums.Authentication;

namespace LSP.Entity.DTO
{
    public class MfaCodeDto : IDto
    {
        [Required]
        public required string UserControlCode { get; set; }
        // [Required]
        // public MfaPurposeEnum Type { get; set; }
    }
}
