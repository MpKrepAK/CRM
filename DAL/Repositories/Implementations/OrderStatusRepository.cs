using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class OrderStatusRepository : IRepository<OrderStatus>
{
    private CRMContext _context;
    public OrderStatusRepository()
    {
        _context = new CRMContext();
    }
    public async Task<List<OrderStatus>> GetAll()
    {
        var list = await _context.OrderStatuses
            .Include(x=>x.Orders)
            .ToListAsync();
        return list;
    }

    public async Task<OrderStatus> GetById(int id)
    {
        var list = await _context.OrderStatuses
            .Include(x=>x.Orders)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(OrderStatus entity)
    {
        try
        {
            entity.Id = 0;
            _context.OrderStatuses.Add(entity);
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
            var e = await _context.OrderStatuses.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.OrderStatuses.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(int id, OrderStatus entity)
    {
        try
        {
            var e = await _context.OrderStatuses
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.Name = entity.Name;
            _context.OrderStatuses.Update(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}