using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations;

public class OrdersRepository : IRepository<Order>
{
    private CRMContext _context;
    public OrdersRepository()
    {
        _context = new CRMContext();
    }
    public async Task<List<Order>> GetAll()
    {
        var list = await _context.Orders
            .Include(x=>x.Product)
            .Include(x=>x.Customer)
            .Include(x=>x.OrderStatus)
            .ToListAsync();
        return list;
    }

    public async Task<Order> GetById(int id)
    {
        var list = await _context.Orders
            .Include(x=>x.Product)
            .Include(x=>x.Customer)
            .Include(x=>x.OrderStatus)
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }

    public async Task<bool> Add(Order entity)
    {
        try
        {
            entity.Id = 0;
            _context.Orders.Add(entity);
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
            var e = await _context.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Orders.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<bool> Update(int id, Order entity)
    {
        try
        {
            var e = await _context.Orders
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.ProductId = entity.ProductId;
            e.CustomerId = entity.CustomerId;
            e.OrderStatusId = entity.OrderStatusId;
            e.DateTime = entity.DateTime;
            _context.Orders.Update(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}