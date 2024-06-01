using LSP.Core.Entities;

namespace LSP.Entity.DTO.Department
{
    public class DepartmentDto : IDto
    {
        public required string Name { get; set; }
        public required string FacultyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}