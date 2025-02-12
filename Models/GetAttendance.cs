namespace Register.Models
{
    public class GetAttendance
    {
        public required Guid UserId { get; set; }
        public required String Date { get; set; }
    }
}
