using System.ComponentModel.DataAnnotations.Schema;

namespace Register.Models.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime CameTime { get; set; } = DateTime.UtcNow;
        public DateTime LeftTime { get; set; } = DateTime.UtcNow;
        public bool IsUpdated { get; set; } = false;
        public string? Reason { get; set; }

        [ForeignKey("UserId")]
        public Signup? User { get; set; }
    }
}
