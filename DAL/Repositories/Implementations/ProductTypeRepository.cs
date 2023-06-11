using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class ProductTypeRepository : IRepository<ProductType>
{
    private CRMContext _context;
    private ILogger _logger;
    public ProductTypeRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    public async Task<List<ProductType>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - ProductTypeRepository");
        var list = await _context.ProductTypes
            .Include(x=>x.Products)
            .ToListAsync();
        return list;
    }

    public async Task<ProductType> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - ProductTypeRepository");
        var list = await _context.ProductTypes
            .Include(x=>x.Products)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(ProductType entity)
    {
        _logger.Log("Вызван метод Add - ProductTypeRepository");
        try
        {
            entity.Id = 0;
            _context.ProductTypes.Add(entity);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Add - ProductTypeRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Add - ProductTypeRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        _logger.Log("Вызван метод Delete - ProductTypeRepository");
        try
        {
            var e = await _context.ProductTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.ProductTypes.Remove(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Delete - ProductTypeRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Delete - ProductTypeRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Update(int id, ProductType entity)
    {
        _logger.Log("Вызван метод Update - ProductTypeRepository");
        try
        {
            var e = await _context.ProductTypes
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.Name = entity.Name;
            _context.ProductTypes.Update(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Update - ProductTypeRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Update - ProductTypeRepository -"+e.Message);
            return false;
        }
    }
}