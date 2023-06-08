using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitys.Database;

public class RoleTask
{
    public int Id { get; set; }
    public string Tittle { get; set; }
    public string Body { get; set; }
    [ForeignKey("Role")]
    public int RoleId { get; set; }
    public Role Role { get; set; }
}