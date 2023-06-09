using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Entitys.Database;

public class UserChat
{
    public int Id { get; set; }
    public string Message { get; set; }
    [ForeignKey("Sender")]
    public int SenderId { get; set; }
    public User Sender { get; set; }
    public DateTime DateTime { get; set; }
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append(this.Id + " " + Sender.FirstName + " " + Sender.LastName+"\n");
        sb.Append(this.DateTime.ToString()+"\n");
        sb.Append("\t"+this.Message);        
        sb.Append("\n");

        return sb.ToString();
    }
}