using System.ComponentModel.DataAnnotations;
using LSP.Core.Entities;
using LSP.Core.Utilities.CustomAttributes;
using LSP.Entity.DTO.Authentication;

namespace LSP.Entity.DTO
{
	public class SecurityResponseDto : IDto
	{
		[Required]
		public string UserControlCode { get; set; }
		[Required]
		[NotEmptyList]
		public List<MfaTypeResponseDto> MfaTypes { get; set; }
	}
}