using System.Windows;
using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using BLL.Models.Services.Proxy;
using BLL.Models.Services.Validation;
using Exception = System.Exception;

namespace DAL.Repositories.Implementations;

public class CustomerRepository : IRepository<Customer>, IAttributeSerializeble
{
    
    private CRMContext _context;
    private ILogger _logger;
    public CustomerRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    
    public async Task<List<Customer>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - CustomerRepository");
        var list = await _context.Customers
            .ToListAsync();
        return list;
    }
    
    public async Task<Customer> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - CustomerRepository");
        var list = await _context.Customers
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }
    
    public async Task<bool> Add(Customer entity)
    {
        _logger.Log("Вызван метод Add - CustomerRepository");
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

            if (entity.Age<=0)
            {
                return false;
            }
            entity.Id = 0;
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Add - CustomerRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Add - CustomerRepository -"+e.Message);
            return false;
        }
    }
    
    public async Task<bool> Delete(int id)
    {
        _logger.Log("Вызван метод Delete - CustomerRepository");
        try
        {
            var e = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Customers.Remove(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Delete - CustomerRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Delete - CustomerRepository -"+e.Message);
            return false;
        }
    }
    
    public async Task<bool> Update(int id, Customer entity)
    {
        _logger.Log("Вызван метод Update - CustomerRepository");
        try
        {
            var e = await _context.Customers
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
            if (entity.Age<=0)
            {
                return false;
            }
            e.Email = entity.Email;
            e.PhoneNumber = entity.PhoneNumber;
            e.FirstName = entity.FirstName;
            e.MidleName = entity.MidleName;
            e.LastName = entity.LastName;
            e.VK = entity.VK;
            e.Instagram = entity.Instagram;
            e.Telegram = entity.Telegram;
            e.Facebook = entity.Facebook;
            e.HeardAboutUsFrom = entity.HeardAboutUsFrom;
            e.Age = entity.Age;
            e.DateOfRegistration = entity.DateOfRegistration;
            _context.Customers.Update(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Update - CustomerRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Update - CustomerRepository -"+e.Message);
            return false;
        }
    }
}