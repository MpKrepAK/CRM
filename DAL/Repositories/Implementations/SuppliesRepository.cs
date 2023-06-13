using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using BLL.Models.Services.Validation;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class SuppliesRepository : IRepository<Supplies>
{
    private CRMContext _context;
    private ILogger _logger;

    public SuppliesRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    public async Task<List<Supplies>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - ProviderRepository");
        var list = await _context.Supplieses
            .Include(x=>x.Product)
            .ToListAsync();
        return list;
    }

    public async Task<Supplies> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - ProviderRepository");
        var list = await _context.Supplieses
            .Include(x=>x.Product)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Supplies entity)
    {
        _logger.Log("Вызван метод Add - ProviderRepository");
        try
        {
            var ware = await _context.Warehouses.FirstOrDefaultAsync(x => x.ProductId == entity.ProductId);
            if (ware == null)
            {
                return false;
            }
            long l;
            if (entity.Count<=0)
            {
                return false;
            }

            ware.Count += entity.Count;
            entity.Id = 0;
            _context.Supplieses.Add(entity);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Add - ProviderRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Add - ProviderRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        _logger.Log("Вызван метод Delete - ProductTypeRepository");
        try
        {
            var e = await _context.Supplieses.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Supplieses.Remove(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Delete - ProviderRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Delete - ProviderRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Update(int id, Supplies entity)
    {
        _logger.Log("Вызван метод Update - ProviderRepository");
        try
        {
            var e = await _context.Supplieses
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }
            
            if (entity.Count<=0)
            {
                return false;
            }

            e.Count = entity.Count;
            e.DateTime = entity.DateTime;
            _context.Supplieses.Update(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Update - ProviderRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Update - ProviderRepository -"+e.Message);
            return false;
        }
    }
}