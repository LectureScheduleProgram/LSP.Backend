using LSP.Core.Entities;

namespace LSP.Entity.DTO.Authentication
{
    public class GoogleAuthenticatorDto : IDto
    {
        public string QrCodeSetupImageUrl { get; set; }
        public string ManualEntryKey { get; set; }
        public string UserUniqueKey { get; set; }
    }
}
