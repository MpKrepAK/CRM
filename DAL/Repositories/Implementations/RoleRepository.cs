using BLL.Models.Services.Proxy;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class RoleRepository : IRepository<Role>, IAttributeSerializeble
{
    
    private CRMContext _context;
    public RoleRepository()
    {
        _context = new CRMContext();
    }
    
    public async Task<List<Role>> GetAll()
    {
        var list = await _context.Roles
            .Include(x=>x.Users)
            .ToListAsync();
        return list;
    }
    
    public async Task<Role> GetById(int id)
    {
        var e = await _context.Roles
            .Include(x=>x.Users)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return e;
    }
    
    public async Task<bool> Add(Role entity)
    {
        try
        {
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
        
    }
    
    public async Task<bool> Delete(int id)
    {
        try
        {
            var e = await _context.Roles.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Roles.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public async Task<bool> Update(int id, Role entity)
    {
        try
        {
            var e = await _context.Roles
                .Include(x=>x.Users)
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.Name = entity.Name;
            e.Value = entity.Value;
            _context.Roles.Update(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}