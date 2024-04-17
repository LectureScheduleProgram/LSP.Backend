using LSP.Core.Entities;

namespace LSP.Entity.DTO.Authentication
{
    public class SecurityCodeResponseDto : IDto
    {
        public DateTime? ExpireDate { get; set; }
        public string SentTo { get; set; }
    }
}
