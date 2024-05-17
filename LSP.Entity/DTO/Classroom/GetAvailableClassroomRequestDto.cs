using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LSP.Core.Entities;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Entity.DTO.Lecture
{
    public class GetAvailableClassroomRequestDto : IDto
    {
        [DefaultValue(1)]
        public required byte ClassroomTypeId { get; set; }

        [DefaultValue(1)]
        public required byte ClassroomCapacityId { get; set; }

        [DefaultValue(DaysEnum.Monday)]
        [Required]
        public required DaysEnum Day { get; set; }

        [Range(1, 24)]
        [DefaultValue((byte)1)]
        public required byte StartHour { get; set; }

        [DefaultValue((byte)1)]
        [Range(1, 24)]
        public required byte EndHour { get; set; }
    }
}