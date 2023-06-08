namespace DAL.Entitys.Database;

public class ProductType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Product>? Products { get; set; }
    public override string ToString()
    {
        return this.Id+"\t"+this.Name;
    }
}