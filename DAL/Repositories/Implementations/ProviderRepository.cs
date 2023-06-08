using System.Windows;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class ProviderRepository : IRepository<Provider>
{
    private CRMContext _context;
    public ProviderRepository()
    {
        _context = new CRMContext();
    }
    public async Task<List<Provider>> GetAll()
    {
        var list = await _context.Providers
            .Include(x=>x.Products)
            .ToListAsync();
        return list;
    }

    public async Task<Provider> GetById(int id)
    {
        var list = await _context.Providers
            .Include(x=>x.Products)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Provider entity)
    {
        try
        {
            entity.Id = 0;
            _context.Providers.Add(entity);
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
            var e = await _context.Providers.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Providers.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(int id, Provider entity)
    {
        try
        {
            var e = await _context.Providers
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.Email = entity.Email;
            e.UrName = entity.UrName;
            e.PhoneNumber = entity.PhoneNumber;
            _context.Providers.Update(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}