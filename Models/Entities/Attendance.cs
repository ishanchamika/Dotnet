using System.ComponentModel.DataAnnotations.Schema;

namespace Register.Models.Entities
{
    public class Attendance
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public String Date { get; set; }
        public String? CameTime { get; set; }
        public String? LeftTime { get; set; }
        public bool? IsUpdated { get; set; } = false;
        public string? Reason { get; set; }

        [ForeignKey("UserId")]
        public Signup? User { get; set; }
    }
}
