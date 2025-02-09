namespace Register.Models
{
    public class AddAttendanceDto
    {
        public required Guid UserId { get; set; }
        public DateTime? CameTime { get; set; }
        public DateTime? LeftTime { get; set; }
        public bool IsUpdated { get; set; } = false;
        public string? Reason { get; set; }
    }
}
