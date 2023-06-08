using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entitys.Database;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Info { get; set; }
    public double Cost { get; set; }
    [ForeignKey("ProductType")]
    public int ProductTypeId { get; set; }
    public ProductType ProductType { get; set; }
    [ForeignKey("Provider")]
    public int ProviderId { get; set; }
    public Provider Provider { get; set; }
}