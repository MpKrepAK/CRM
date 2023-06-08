using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class ProductRepository : IRepository<Product>
{
    private CRMContext _context;
    public ProductRepository()
    {
        _context = new CRMContext();
    }
    
    public async Task<List<Product>> GetAll()
    {
        var list = await _context.Products
            .Include(x=>x.ProductType)
            .Include(x=>x.Provider)
            .ToListAsync();
        return list;
    }

    public async Task<Product> GetById(int id)
    {
        var list = await _context.Products
            .Include(x=>x.ProductType)
            .Include(x=>x.Provider)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Product entity)
    {
        try
        {
            entity.Id = 0;
            _context.Products.Add(entity);
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
            var e = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Products.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(int id, Product entity)
    {
        try
        {
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
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}