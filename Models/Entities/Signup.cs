namespace Register.Models.Entities
{
    public class Signup
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public ICollection<Attendance>? Attendances { get; set; }
    }
}
