using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class ProductRepository : IRepository<Product>
{
    private CRMContext _context;
    private ILogger _logger;
    public ProductRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    
    public async Task<List<Product>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - ProductRepository");
        var list = await _context.Products
            .Include(x=>x.ProductType)
            .Include(x=>x.Provider)
            .ToListAsync();
        return list;
    }

    public async Task<Product> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - ProductRepository");
        var list = await _context.Products
            .Include(x=>x.ProductType)
            .Include(x=>x.Provider)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Product entity)
    {
        _logger.Log("Вызван метод Add - ProductRepository");
        try
        {
            if (entity.Cost<=0)
            {
                return false;
            }
            entity.Id = 0;
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Add - ProductRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Add - ProductRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        _logger.Log("Вызван метод Delete - ProductRepository");
        try
        {
            var e = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Products.Remove(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Delete - ProductRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Delete - ProductRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Update(int id, Product entity)
    {
        _logger.Log("Вызван метод Update - ProductRepository");
        try
        {
            if (entity.Cost<=0)
            {
                return false;
            }
            var e = await _context.Products
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.ProviderId = entity.ProviderId;
            e.Name = entity.Name;
            e.Info = entity.Info;
            e.Cost = entity.Cost;
            e.ProductTypeId = entity.ProductTypeId;
            _context.Products.Update(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Update - ProductRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Update - ProductRepository -"+e.Message);
            return false;
        }
    }
}