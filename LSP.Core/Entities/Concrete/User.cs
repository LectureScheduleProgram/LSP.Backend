using System.ComponentModel.DataAnnotations;

namespace LSP.Core.Entities.Concrete
{
	public class User : IEntity
	{
		[Key]
		public int Id { get; set; }
		public string? Email { get; set; }
		public string? Name { get; set; }
		public string? Surname { get; set; }
		public byte[]? PasswordSalt { get; set; }
		public byte[]? PasswordHash { get; set; }
		public DateTime CreatedDate { get; set; }
		public byte Status { get; set; }
		public string? PhoneNumber { get; set; }
		public string? SecurityCode { get; set; }
	}
}
