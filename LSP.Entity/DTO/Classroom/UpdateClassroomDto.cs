using LSP.Core.Entities;

namespace LSP.Entity.DTO.Classroom
{
    public class UpdateClassroomDto : IDto
    {
        public required short Id { get; set; }
        public required string Name { get; set; }
    }
}