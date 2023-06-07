namespace DAL.Entitys.Database;

public class Order
{
    public int Id { get; set; }
    public Customer Customer { get; set; }
    public List<Product> Products { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime DateTime { get; set; }
}