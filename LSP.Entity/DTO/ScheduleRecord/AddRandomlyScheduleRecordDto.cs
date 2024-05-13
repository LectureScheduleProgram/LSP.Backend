using System.ComponentModel;
using LSP.Core.Entities;

namespace LSP.Entity.DTO.ScheduleRecord
{
    public class AddRandomlyScheduleRecordDto : IDto
    {
        [DefaultValue(1)]
        public required byte ClassroomTypeId { get; set; }
        [DefaultValue(1)]
        public required byte ClassroomCapacityId { get; set; }
        [DefaultValue(1)]
        public required byte HourOfLecture { get; set; }
    }
}