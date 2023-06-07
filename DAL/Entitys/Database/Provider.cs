namespace DAL.Entitys.Database;

public class Provider
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<Product> Products { get; set; }
}