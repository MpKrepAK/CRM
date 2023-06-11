using System.Windows;
using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using BLL.Models.Services.Validation;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class UserRepository : IRepository<User>
{
    private CRMContext _context;
    private ILogger _logger;
    public UserRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    public async Task<List<User>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - UserRepository");
        var list = await _context.Users
            .ToListAsync();
        return list;
    }

    public async Task<User> GetById(int id)
    {
        _logger.Log("Вызван метод GetById - UserRepository");
        var list = await _context.Users
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(User entity)
    {
        _logger.Log("Вызван метод Add - UserRepository");
        try
        {
            var ent = await _context.Users.FirstOrDefaultAsync(x => x.Login == entity.Login);
            if (ent!=null)
            {
                return false;
            }
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Add - UserRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Add - UserRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        _logger.Log("Вызван метод Delete - UserRepository");
        try
        {
            var e = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Users.Remove(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Delete - UserRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Delete - UserRepository -"+e.Message);
            return false;
        }
    }

    public async Task<bool> Update(int id, User entity)
    {
        _logger.Log("Вызван метод Update - UserRepository");
        try
        {
            var e = await _context.Users
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.FirstName = entity.FirstName;
            e.MidleName = entity.MidleName;
            e.LastName = entity.LastName;
            e.Login = entity.Login;
            e.Password = entity.Password;
            e.DiskName = entity.DiskName;
            _context.Users.Update(e);
            await _context.SaveChangesAsync();
            _logger.Log("Успешно завершен метод Update - UserRepository");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка при выполнении метода Update - UserRepository -"+e.Message);
            return false;
        }
    }
}