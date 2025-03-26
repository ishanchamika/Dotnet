using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Register.Models.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        public int Current_qid { get; set; }  // Consider whether this is needed

        [ForeignKey("Component")]
        public int ComponentId { get; set; }

        public string Text { get; set; }  // Renamed from `Questions` to `Text`
        public string Note { get; set; }

        public Component Component { get; set; }
    }
}
