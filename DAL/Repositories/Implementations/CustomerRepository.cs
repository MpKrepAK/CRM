using System.Windows;
using DAL.Entitys.Database;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using BLL.Models.Services.Proxy;
using Exception = System.Exception;

namespace DAL.Repositories.Implementations;

public class CustomerRepository : IRepository<Customer>, IAttributeSerializeble
{
    
    private CRMContext _context;
    public CustomerRepository()
    {
        _context = new CRMContext();
    }
    
    public async Task<List<Customer>> GetAll()
    {
        var list = await _context.Customers
            .ToListAsync();
        return list;
    }
    
    public async Task<Customer> GetById(int id)
    {
        var list = await _context.Customers
            .FirstOrDefaultAsync(x=>x.Id == id);
        return list;
    }
    
    public async Task<bool> Add(Customer entity)
    {
        try
        {
            entity.Id = 0;
            _context.Customers.Add(entity);
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
            var e = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
            if (e == null)
            {
                return false;
            }
            _context.Customers.Remove(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    
    public async Task<bool> Update(int id, Customer entity)
    {
        try
        {
            var e = await _context.Customers
                .FirstOrDefaultAsync(x=>x.Id == id);
            if (e == null)
            {
                return false;
            }

            e.Email = entity.Email;
            e.PhoneNumber = entity.PhoneNumber;
            _context.Customers.Update(e);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}