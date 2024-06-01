using LSP.Core.Entities;

namespace LSP.Entity.DTO.Department
{
    public class LectureDto : IDto
    {
        public required short Id { get; set; }
        public required string Name { get; set; }
        public string? DepartmentName { get; set; }
        public string? FacultyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}