using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class WarehouseRepository : IRepository<Warehouse>
{
    private CRMContext _context;
    private ILogger _logger;
    public WarehouseRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    public async Task<List<Warehouse>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - WarehouseRepository");
        var list = await _context.Warehouses
            .Include(x=>x.Product)
            .ToListAsync();
        return list;
    }

    public async Task<Warehouse> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - WarehouseRepository");
        var list = await _context.Warehouses
            .Include(x=>x.Product)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Warehouse entity)
    {
        _logger.Log("Вызван метод Add - WarehouseRepository");
        try
        {
            if (entity.Count<=0)
            {
                return false;
            }
            entity.Id = 0;
            _context.Warehouses.Add(entity);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Add - WarehouseRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Add - WarehouseRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        _logger.Log("Вызван метод Delete - WarehouseRepository");
        try
        {
            var e = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Warehouses.Remove(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Delete - WarehouseRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Delete - WarehouseRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Update(int id, Warehouse entity)
    {
        _logger.Log("Вызван метод Update - WarehouseRepository");
        try
        {
            var e = await _context.Warehouses
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }
            if (entity.Count<=0)
            {
                return false;
            }
            e.ProductId = entity.ProductId;
            e.Count = entity.Count;
            _context.Warehouses.Update(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Update - WarehouseRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Update - WarehouseRepository -"+e.Message);
            return false;
        }
    }
}