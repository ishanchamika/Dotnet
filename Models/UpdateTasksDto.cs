namespace Register.Models
{
    public class UpdateTasksDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string ? TaskName { get; set; }
        public string ? TaskDescription { get; set; }
        public string ? TaskStatus { get; set; }
        public string ? TaskDeadline { get; set; }
    }
}
