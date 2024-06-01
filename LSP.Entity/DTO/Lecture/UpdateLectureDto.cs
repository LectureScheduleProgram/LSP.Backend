using LSP.Core.Entities;

namespace LSP.Entity.DTO.Department
{
    public class UpdateLectureDto : IDto
    {
        public required short Id { get; set; }
        public string? Name { get; set; }
        public short? DepartmentId { get; set; }
    }
}