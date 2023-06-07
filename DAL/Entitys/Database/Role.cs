namespace DAL.Entitys.Database;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }
    public List<User> Users { get; set; }
}