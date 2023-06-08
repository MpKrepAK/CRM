using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitys.Database;

public class UserChat
{
    public int Id { get; set; }
    public string Message { get; set; }
    [ForeignKey("Sender")]
    public int SenderId { get; set; }
    public User Sender { get; set; }
    [ForeignKey("Recipient")]
    public int RecipientId { get; set; }
    public User Recipient { get; set; }
    public DateTime DateTime { get; set; }
}