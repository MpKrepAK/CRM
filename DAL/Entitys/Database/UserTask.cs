using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitys.Database;

public class UserTask
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Body { get; set; }
    [ForeignKey("Sender")]
    public int SenderId { get; set; }
    public User Sender { get; set; }
    [ForeignKey("Recipient")]
    public int RecipientId { get; set; }
    public User Recipient { get; set; }
}