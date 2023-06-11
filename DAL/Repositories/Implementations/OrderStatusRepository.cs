using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class OrderStatusRepository : IRepository<OrderStatus>
{
    private CRMContext _context;
    private ILogger _logger;
    public OrderStatusRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    public async Task<List<OrderStatus>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - OrderStatusRepository");
        var list = await _context.OrderStatuses
            .Include(x=>x.Orders)
            .ToListAsync();
        return list;
    }

    public async Task<OrderStatus> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - OrderStatusRepository");
        var list = await _context.OrderStatuses
            .Include(x=>x.Orders)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(OrderStatus entity)
    {
        _logger.Log("Вызван метод Add - OrderStatusRepository");
        try
        {
            entity.Id = 0;
            _context.OrderStatuses.Add(entity);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Add - OrderStatusRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Add - OrderStatusRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        _logger.Log("Вызван метод Delete - OrderStatusRepository");
        try
        {
            var e = await _context.OrderStatuses.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.OrderStatuses.Remove(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Delete - OrderStatusRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Delete - OrderStatusRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Update(int id, OrderStatus entity)
    {
        _logger.Log("Вызван метод Update - OrderStatusRepository");
        try
        {
            var e = await _context.OrderStatuses
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.Name = entity.Name;
            _context.OrderStatuses.Update(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Update - OrderStatusRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Update - OrderStatusRepository -"+e.Message);
            return false;
        }
    }
}