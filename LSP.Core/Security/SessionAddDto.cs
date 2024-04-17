using LSP.Core.Entities;

namespace LSP.Core.Security
{
    public class SessionAddDto : IDto
    {
        public int UserId { get; set; }
        public string? TokenString { get; set; }
        public string? SiteType { get; set; }
        public string? UserAgent { get; set; }
        public string? Ip { get; set; }
    }
}
