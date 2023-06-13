using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitys.Database;

public class Supplies
{
    public int Id { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }
    public Product Product { get; set; }
    public DateTime DateTime { get; set; }
    public int Count { get; set; }
}