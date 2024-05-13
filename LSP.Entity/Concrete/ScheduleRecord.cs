using LSP.Core;
using LSP.Entity.Abstract.Common;
using LSP.Entity.Enum.ScheduleRecord;

namespace LSP.Entity.Concrete
{
    public class ScheduleRecord : BaseEntity, IEntity
    {
        public int Id { get; set; }
        // public Classroom Classroom { get; set; }
        public short ClassroomId { get; set; }
        // public Lecture Lecture { get; set; }
        public short LectureId { get; set; }
        public DaysEnum Day { get; set; }
        public TimeSpan StartingTime { get; set; }
        public TimeSpan EndingTime { get; set; }
        public ScheduleRecordStatusEnum Status { get; set; }
    }
}