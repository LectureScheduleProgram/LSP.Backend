using System.ComponentModel;
using LSP.Core.Entities;

namespace LSP.Entity.DTO.Lecture
{
    public class GetAvailableClassroomResponseDto : IDto
    {
        public short Id { get; set; }
        public required string Name { get; set; }
        public required string ClassroomType { get; set; }
        public required short ClassroomCapacity { get; set; }
    }
}