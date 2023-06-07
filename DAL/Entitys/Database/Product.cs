namespace DAL.Entitys.Database;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Cost { get; set; }
    public ProductType ProductType { get; set; }
    public Provider Provider { get; set; }
}