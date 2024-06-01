using LSP.Core.Entities;

namespace LSP.Entity.DTO.Department
{
    public class AddDepartmentDto : IDto
    {
        public required string Name { get; set; }
        public required short FacultyId { get; set; }
    }
}