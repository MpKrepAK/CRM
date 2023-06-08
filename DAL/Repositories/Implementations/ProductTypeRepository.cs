using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class ProductTypeRepository : IRepository<ProductType>
{
    private CRMContext _context;
    public ProductTypeRepository()
    {
        _context = new CRMContext();
    }
    public async Task<List<ProductType>> GetAll()
    {
        var list = await _context.ProductTypes
            .Include(x=>x.Products)
            .ToListAsync();
        return list;
    }

    public async Task<ProductType> GetById(int id)
    {
        var list = await _context.ProductTypes
            .Include(x=>x.Products)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(ProductType entity)
    {
        try
        {
            entity.Id = 0;
            _context.ProductTypes.Add(entity);
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
            var e = await _context.ProductTypes
                .FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.ProductTypes.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(int id, ProductType entity)
    {
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
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}