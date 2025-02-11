using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Register.Models
{
    public class AddAttendanceDto
    {
        public required Guid UserId { get; set; }
        public required String Date {  get; set; }
        public String ? CameTime { get; set; }
        public  String ? LeftTime { get; set; }
        public bool ? IsUpdated { get; set; } = false;
        public string ? Reason { get; set; }
    }
}