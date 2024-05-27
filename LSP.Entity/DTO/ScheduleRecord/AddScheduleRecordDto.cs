using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Entity.DTO.ScheduleRecord
{
    public class AddScheduleRecordDto
    {
        [DefaultValue(1)]
        public required byte ClassroomId { get; set; }
        [DefaultValue(1)]
        public required byte LectureId { get; set; }

        [DefaultValue(DaysEnum.Monday)]
        public required DaysEnum Day { get; set; }

        [Range(1, 24)]
        [DefaultValue((byte)1)]
        public required byte StartHour { get; set; }

        [DefaultValue((byte)1)]
        [Range(1, 24)]
        public required byte EndHour { get; set; }
    }
}