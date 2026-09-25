using System.Linq.Expressions;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories;

public interface IBaseRepository<T> where T : class, ISqlEntity
{
    Task InsertAsync(T entity);
    Task Insert(T entity);
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T?> FindAsync(Expression<Func<T, bool>> predicate);
    Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    void Update(T entity);
    Task<bool> UpdateAsync(T entity, T newValue);
    Task<bool> UpdateByIdAsync(int id, T newValue);
    void Delete(T entity);
    Task<bool> DeleteAsync(T entity);
    Task<bool> DeleteByIdAsync(int id);
    Task<int> WhereDeleteAsync(Expression<Func<T, bool>> predicate);
    Task<bool> SaveChangesAsync();
    Task<int> CountAsync(Expression<Func<T, bool>> predicate);
    Task<long> SumAsync(Expression<Func<T, bool>> predicate,Expression<Func<T, long>> selector);
    string ToCamelCase(string s);
    Task<PaginateResponseDto<T>> Paginate(BaseFilterRequest filter,IQueryable<T> queryable);
}