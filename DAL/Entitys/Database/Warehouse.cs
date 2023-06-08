using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitys.Database;

public class Warehouse
{
    public int Id { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public long Count { get; set; }
}