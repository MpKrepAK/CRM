using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class WarehouseRepository : IRepository<Warehouse>
{
    private CRMContext _context;
    public WarehouseRepository()
    {
        _context = new CRMContext();
    }
    public async Task<List<Warehouse>> GetAll()
    {
        var list = await _context.Warehouses
            .Include(x=>x.Product)
            .ToListAsync();
        return list;
    }

    public async Task<Warehouse> GetById(int id)
    {
        var list = await _context.Warehouses
            .Include(x=>x.Product)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Warehouse entity)
    {
        try
        {
            entity.Id = 0;
            _context.Warehouses.Add(entity);
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
            var e = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Warehouses.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(int id, Warehouse entity)
    {
        try
        {
            var e = await _context.Warehouses
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.ProductId = entity.ProductId;
            e.Count = entity.Count;
            _context.Warehouses.Update(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}