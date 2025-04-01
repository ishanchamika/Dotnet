using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Register.Models.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public int Current_qid { get; set; }
        public int ComponentId { get; set; }
        public string? Text { get; set; }
        public string? Note { get; set; }

        //[ForeignKey("ComponentId")]
        //public Component? Component { get; set; }
    }
}
