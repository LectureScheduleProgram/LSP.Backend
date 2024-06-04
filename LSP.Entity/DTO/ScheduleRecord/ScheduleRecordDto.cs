using System.ComponentModel.DataAnnotations;
using LSP.Core.Entities;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Entity.DTO.ScheduleRecord
{
    public class ScheduleRecordDto : IDto
    {
        public int Id { get; set; }
        public required string ClassroomName { get; set; }
        public required string LectureName { get; set; }
        public DaysEnum Day { get; set; }
        [Range(1, 24)]
        public byte StartHour { get; set; }
        [Range(1, 24)]
        public byte EndHour { get; set; }
    }
}