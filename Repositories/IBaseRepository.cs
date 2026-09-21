using System.Linq.Expressions;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories;

public interface IBaseRepository<T> where T : class, ISqlEntity
{
    Task InsertAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    Task<bool> UpdateAsync(T entity, T newValue);
    Task<bool> UpdateByIdAsync(int id, T newValue);
    Task<bool> DeleteAsync(T entity);
    Task<bool> DeleteByIdAsync(int id);
    Task<int> WhereDeleteAsync(Expression<Func<T, bool>> predicate);
    Task<bool> SaveChangesAsync();
}