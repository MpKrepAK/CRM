using System.Windows;
using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using BLL.Models.Services.Validation;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class ProviderRepository : IRepository<Provider>
{
    private CRMContext _context;
    private ILogger _logger;
    public ProviderRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    public async Task<List<Provider>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - ProviderRepository");
        var list = await _context.Providers
            .Include(x=>x.Products)
            .ToListAsync();
        return list;
    }

    public async Task<Provider> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - ProviderRepository");
        var list = await _context.Providers
            .Include(x=>x.Products)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Provider entity)
    {
        _logger.Log("Вызван метод Add - ProviderRepository");
        try
        {
            long l;
            if (!long.TryParse(entity.PhoneNumber, out l))
            {
                return false;
            }

            if (!EmailValidator.IsValidEmail(entity.Email))
            {
                return false;
            }
            entity.Id = 0;
            _context.Providers.Add(entity);
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
            var e = await _context.Providers.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Providers.Remove(e);
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

    public async Task<bool> Update(int id, Provider entity)
    {
        _logger.Log("Вызван метод Update - ProviderRepository");
        try
        {
            var e = await _context.Providers
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }
            long l;
            if (!long.TryParse(entity.PhoneNumber, out l))
            {
                return false;
            }

            if (!EmailValidator.IsValidEmail(entity.Email))
            {
                return false;
            }
            e.Email = entity.Email;
            e.UrName = entity.UrName;
            e.PhoneNumber = entity.PhoneNumber;
            _context.Providers.Update(e);
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