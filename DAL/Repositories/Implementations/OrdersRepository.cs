using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class OrdersRepository : IRepository<Order>
{
    private CRMContext _context;
    private ILogger _logger;
    public OrdersRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    public async Task<List<Order>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - OrdersRepository");
        var list = await _context.Orders
            .Include(x=>x.Product)
            .ThenInclude(x=>x.ProductType)
            .Include(x=>x.Customer)
            .Include(x=>x.OrderStatus)
            .ToListAsync();
        return list;
    }

    public async Task<Order> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - OrdersRepository");
        var list = await _context.Orders
            .Include(x=>x.Product)
            .Include(x=>x.Customer)
            .Include(x=>x.OrderStatus)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Order entity)
    {
        _logger.Log("Вызван метод Add - OrdersRepository");
        try
        {
            entity.Id = 0;
            _context.Orders.Add(entity);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Add - OrdersRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Add - OrdersRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        _logger.Log("Вызван метод Delete - OrdersRepository");
        try
        {
            var e = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Orders.Remove(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Delete - OrdersRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Delete - OrdersRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Update(int id, Order entity)
    {
        _logger.Log("Вызван метод Update - OrdersRepository");
        try
        {
            var e = await _context.Orders
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.ProductId = entity.ProductId;
            e.CustomerId = entity.CustomerId;
            e.OrderStatusId = entity.OrderStatusId;
            e.DateTime = entity.DateTime;
            _context.Orders.Update(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Update - OrdersRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Update - OrdersRepository -"+e.Message);
            return false;
        }
    }
}