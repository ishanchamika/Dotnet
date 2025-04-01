using Register.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class QuestionMap
{
    [Key]
    public int Id { get; set; }
    public string Note { get; set; }
    public int Current_qid { get; set; }
    public int Next_qid { get; set; }
    public int Prev_qid { get; set; }
    public int ComponentId { get; set; }

    //[ForeignKey(nameof(Current_qid))]
    //public Question? CurrentQuestion { get; set; }

    //[ForeignKey(nameof(ComponentId))]
    //public Component? Component { get; set; }
}