using LSP.Core.Entities;

namespace LSP.Entity.DTO.Department
{
    public class AddLectureDto : IDto
    {
        public required string Name { get; set; }
        public short? DepartmentId { get; set; }
    }
}