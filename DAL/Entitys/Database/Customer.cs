using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL.Entitys.Database;

public class Customer
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FirstName { get; set; }
    public string? MidleName { get; set; }
    public string? LastName { get; set; }
    public string? VK { get; set; }
    public string? Instagram { get; set; }
    public string? Telegram { get; set; }
    public string? Facebook { get; set; }
    public int? Age { get; set; }
    public DateTime DateOfRegistration { get; set; }
    public string HeardAboutUsFrom { get; set; }
    public override string ToString()
    {
        return this.Id+"\t"+this.Email;
    }
}