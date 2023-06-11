using System.Windows;
using BLL.Models.Services.Logging.Implementations;
using BLL.Models.Services.Logging.Interfaces;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class ChatRepository : IRepository<UserChat>
{
    private CRMContext _context;
    private ILogger _logger;
    public ChatRepository()
    {
        _logger = new FileLogger();
        _context = new CRMContext();
    }
    
    public async Task<List<UserChat>> GetAll()
    {
        _logger.Log("Вызван метод GetAll - ChatRepository");
        var list = await _context.UserChats
            .Include(x=>x.Sender)
            .ToListAsync();
        return list;
    }

    public async Task<UserChat> GetById(int id)
    {
        var list = await _context.UserChats
            .Include(x=>x.Sender)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(UserChat entity)
    {
        try
        {
            entity.Id = 0;
            _context.UserChats.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message);
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            var e = await _context.UserChats.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.UserChats.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(int id, UserChat entity)
    {
        try
        {
            var e = await _context.UserChats
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.Message = entity.Message;
            e.DateTime = entity.DateTime;
            e.SenderId = entity.SenderId;
            _context.UserChats.Update(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}