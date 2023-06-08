using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DAL.Entitys.Database;

public class Provider
{
    public int Id { get; set; }
    public string UrName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<Product>? Products { get; set; }
    public override string ToString()
    {
        return this.Id+"\t"+this.UrName;
    }
}