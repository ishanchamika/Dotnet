using System.ComponentModel.DataAnnotations.Schema;

namespace Register.Models.Entities
{
    public class Tasks
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string ? TaskName { get; set; }
        public string ? TaskDescription { get; set; }
        public string ? TaskStatus { get; set; }
        public string ? TaskDeadline { get; set; }

        [ForeignKey("UserId")]
        public Signup? User { get; set; }
    }
}
