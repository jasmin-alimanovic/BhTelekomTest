using BhTelekomTest.Model;

namespace BhTelekomTest.Interfaces;

public interface IBaseService<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetAsync(PrimaryKey<T> primaryKey);
    Task<T> CreateAsync(T entity);
    Task EditAsync(PrimaryKey<T> primaryKey, T entity);
    Task DeleteAsync(PrimaryKey<T> primaryKey);

}
