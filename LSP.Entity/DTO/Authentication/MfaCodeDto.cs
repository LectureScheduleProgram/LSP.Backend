using LSP.Core.Entities;
using System.ComponentModel.DataAnnotations;

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
