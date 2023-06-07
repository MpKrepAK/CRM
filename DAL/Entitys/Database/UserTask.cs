namespace DAL.Entitys.Database;

public class UserTask
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Body { get; set; }
    public User User { get; set; }
}