using LSP.Core.Entities;

namespace LSP.Entity.DTO.User
{
    public class UserInformationDto : IDto
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreatedDate { get; set; }
        public byte Status { get; set; }
        public string PhoneNumber { get; set; }
    }
}
