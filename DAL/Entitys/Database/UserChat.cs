namespace DAL.Entitys.Database;

public class UserChat
{
    public int Id { get; set; }
    public string Message { get; set; }
    public User Sender { get; set; }
    public User Recipient { get; set; }
    public DateTime DateTime { get; set; }
}