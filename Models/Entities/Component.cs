using System.ComponentModel.DataAnnotations;

namespace Register.Models.Entities
{
    public class Component
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
