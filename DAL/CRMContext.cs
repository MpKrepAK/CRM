using DAL.Entitys.Database;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class CRMContext : DbContext
{
    public DbSet<User> Users { get; set; }=null!;
    public DbSet<Order> Orders { get; set; }=null!;
    public DbSet<Customer> Customers { get; set; }=null!;
    public DbSet<Product> Products { get; set; }=null!;
    public DbSet<ProductType> ProductTypes { get; set; }=null!;
    public DbSet<Provider> Providers { get; set; }=null!;
    public DbSet<Warehouse> Warehouses { get; set; }=null!;
    public DbSet<UserChat> UserChats { get; set; }=null!;
    public DbSet<OrderStatus> OrderStatuses { get; set; }=null!;

    public CRMContext() : base()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;user=root;password=toor;database=crm;", 
            new MySqlServerVersion(new Version(8, 0, 33)));
    }
}