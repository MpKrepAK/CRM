using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitys.Database;

public class Order
{
    public int Id { get; set; }
    [ForeignKey("Customer")]
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    [ForeignKey("OrderStatus")]
    public int OrderStatusId { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public DateTime DateTime { get; set; }
}