using LSP.Core.Entities;

namespace LSP.Entity.DTO.ClassroomCapacity
{
    public class AddClassroomDto : IDto
    {
        public required string Name { get; set; } = Guid.NewGuid().ToString();
        public required byte ClassroomTypeId { get; set; }
        public required byte ClassroomCapacityId { get; set; }
    }
}