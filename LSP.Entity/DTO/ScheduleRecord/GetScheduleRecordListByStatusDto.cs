using LSP.Core.Entities;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Entity.DTO.ScheduleRecord
{
    public class GetScheduleRecordListByStatusDto : IDto
    {
        public int Id { get; set; }
        public required string ClassroomName { get; set; }
        public required short LectureName { get; set; }
        public DaysEnum Day { get; set; }
        public TimeSpan StartingTime { get; set; }
        public TimeSpan EndingTime { get; set; }
        public ScheduleRecordStatusEnum Status { get; set; }
    }
}