namespace DAL.Repositories.Interfaces;

public interface IRepository<T>
{
    Task<List<T>> GetAll();
    Task<T> GetById(int id);
    Task<bool> Add(T entity);
    Task<bool> Delete(int id);
    Task<bool> Update(int id, T entity);
}