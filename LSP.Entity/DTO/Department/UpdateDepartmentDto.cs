using LSP.Core.Entities;

namespace LSP.Entity.DTO.Department
{
    public class UpdateDepartmentDto : IDto
    {
        public required short Id { get; set; }
        public string? Name { get; set; }
        public short? FacultyId { get; set; }
    }
}