namespace DAL.Entitys.Database;

public class Warehouse
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public long Count { get; set; }
}