using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LSP.Core.Entities;

namespace LSP.Entity.DTO.Lecture
{
    public class GetAvailableClassroomListRequestDto : IDto
    {
        [DefaultValue(1)]
        public required byte ClassroomTypeId { get; set; }
        [DefaultValue(1)]
        public required byte ClassroomCapacityId { get; set; }
        [DefaultValue(1)]
        public required byte HourOfLecture { get; set; }
    }
}