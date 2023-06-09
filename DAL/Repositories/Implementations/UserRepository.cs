using System.Windows;
using BLL.Models.Services.Validation;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class UserRepository : IRepository<User>
{
    private CRMContext _context;
    public UserRepository()
    {
        _context = new CRMContext();
    }
    public async Task<List<User>> GetAll()
    {
        var list = await _context.Users
            .ToListAsync();
        return list;
    }

    public async Task<User> GetById(int id)
    {
        var list = await _context.Users
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(User entity)
    {
        try
        {
            var ent = await _context.Users.FirstOrDefaultAsync(x => x.Login == entity.Login);
            if (ent!=null)
            {
                return false;
            }
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            //MessageBox.Show(e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var e = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Users.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(int id, User entity)
    {
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
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}