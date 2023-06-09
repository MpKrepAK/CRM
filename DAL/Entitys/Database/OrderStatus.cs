namespace DAL.Entitys.Database;

public class OrderStatus
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Order>? Orders { get; set; }
    public override string ToString()
    {
        return this.Id+"\t"+this.Name;
    }
}