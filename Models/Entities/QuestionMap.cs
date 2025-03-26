using Register.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class QuestionMap
{
    [Key]
    public int Id { get; set; }
    public string Note { get; set; }

    [ForeignKey("Question")]
    public int Current_qid { get; set; }

    public int Next_qid { get; set; }
    public int Prev_qid { get; set; }

    [ForeignKey("Component")]
    public int ComponentId { get; set; }

    public Question Question { get; set; }
    public Component Component { get; set; }
}