using System.ComponentModel.DataAnnotations;
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
        [Range(1, 24)]
        public byte StartHour { get; set; }
        [Range(1, 24)]
        public byte EndHour { get; set; }
    }
}