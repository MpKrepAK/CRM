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
    public DbSet<Supplies> Supplieses { get; set; }=null!;

    public CRMContext()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=localhost;user=root;password=toor;database=crm;", 
            new MySqlServerVersion(new Version(8, 0, 33)));
    }

    public void Start(int orders)
    {
        ProductTypes.Add(new ProductType(){Name = "Книга"});
        ProductTypes.Add(new ProductType(){Name = "Тетрадь"});
        ProductTypes.Add(new ProductType(){Name = "Ручка"});
        SaveChanges();
        
        for (int i = 0; i < 10; i++)
        {
            Users.Add(new User()
            {
                FirstName = "Имя "+i,
                MidleName = "Фамилия "+i,
                LastName = "Отчество"+i,
                Login = "123"+i,
                Password = "qwe"+i,
                DiskName = "5B731DA9EC67"
            });
        }
        SaveChanges();
        
        for (int i = 0; i < 10; i++)
        {
            int r = new Random().Next(0,5);
            if (r==0)
            {
                Customers.Add(new Customer()
                {
                    FirstName = "Имя "+i,
                    MidleName = "Фамилия "+i,
                    LastName = "Отчество"+i,
                    Email = "qwe@mail.ru"+i,
                    PhoneNumber = "123456"+i,
                    Age = 18,
                    HeardAboutUsFrom = "Друзья"+i
                });
            }
            else if(r==1)
            {
                Customers.Add(new Customer()
                {
                    DateOfRegistration = new DateTime(2023, 6,2),
                    FirstName = "Имя "+i,
                    MidleName = "Фамилия "+i,
                    LastName = "Отчество"+i,
                    Email = "qwe@mail.ru"+i,
                    PhoneNumber = "123456"+i,
                    Age = 18,
                    HeardAboutUsFrom = "Друзья"+i,
                    VK = "https://vk.com/sluzer"
                });
            }
            else if(r==2)
            {
                Customers.Add(new Customer()
                {
                    DateOfRegistration = new DateTime(2023, 6,2),
                    FirstName = "Имя "+i,
                    MidleName = "Фамилия "+i,
                    LastName = "Отчество"+i,
                    Email = "qwe@mail.ru"+i,
                    PhoneNumber = "123456"+i,
                    Age = 18,
                    HeardAboutUsFrom = "Друзья"+i,
                    VK = "https://vk.com/sluzer",
                    Facebook = "https://vk.com/sluzer"
                });
            }
            else if(r==3)
            {
                Customers.Add(new Customer()
                {
                    DateOfRegistration = new DateTime(2022, 6,2),
                    FirstName = "Имя "+i,
                    MidleName = "Фамилия "+i,
                    LastName = "Отчество"+i,
                    Email = "qwe@mail.ru"+i,
                    PhoneNumber = "123456"+i,
                    Age = 18,
                    HeardAboutUsFrom = "Друзья"+i,
                    VK = "https://vk.com/sluzer",
                    Facebook = "https://vk.com/sluzer",
                    Instagram = "https://vk.com/sluzer"
                });
            }
            else if(r==4)
            {
                Customers.Add(new Customer()
                {
                    DateOfRegistration = new DateTime(2022, 6,2),
                    FirstName = "Имя "+i,
                    MidleName = "Фамилия "+i,
                    LastName = "Отчество"+i,
                    Email = "qwe@mail.ru"+i,
                    PhoneNumber = "123456"+i,
                    Age = 18,
                    HeardAboutUsFrom = "Друзья"+i,
                    VK = "https://vk.com/sluzer",
                    Facebook = "https://vk.com/sluzer",
                    Instagram = "https://vk.com/sluzer",
                    Telegram = "https://vk.com/sluzer"
                });
            }
        }
        SaveChanges();
        
        Providers.Add(new Provider()
        {
            UrName = "ООО Книга",
            PhoneNumber = "1234321",
            Email = "asd1@mail.ru"
        });
        Providers.Add(new Provider()
        {
            UrName = "ООО Крыница",
            PhoneNumber = "1234321",
            Email = "asd2@mail.ru"
        });
        SaveChanges();
        
        OrderStatuses.Add(new OrderStatus()
        {
            Name = "На складе"
        });
        OrderStatuses.Add(new OrderStatus()
        {
            Name = "Доставлен"
        });
        SaveChanges();
        
        for (int i = 0; i < 100; i++)
        {
            Products.Add(new Product()
            {
                Name = i.ToString(),
                ProductTypeId = new Random().Next(1,4),
                ProviderId = new Random().Next(1,3),
                Info = i+"info",
                Cost = new Random().Next(5,15)
            });
        }
        SaveChanges();
        
        for (int i = 0; i < 100; i++)
        {
            Warehouses.Add(new Warehouse()
            {
                ProductId = i+1,
                Count = 10000
            });
        }
        SaveChanges();
    
        for (int i = 0; i < orders; i++)
        {
            Orders.Add(new Order()
            {
                OrderStatusId = new Random().Next(1, 3),
                CustomerId = new Random().Next(1, 10),
                ProductId = new Random().Next(1, 100),
                DateTime = new DateTime(new Random().Next(2020, 2024), new Random().Next(1, 13),
                    new Random().Next(1, 28))
            });
        }
        SaveChanges();
    }
}