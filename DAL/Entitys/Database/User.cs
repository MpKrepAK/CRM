using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitys.Database;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string MidleName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string DiskName { get; set; }
    public List<UserChat>? Chats { get; set; }
}