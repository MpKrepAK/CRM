namespace DAL.Entitys.Database;

public class RoleTask
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Body { get; set; }
    public Role Role { get; set; }
}