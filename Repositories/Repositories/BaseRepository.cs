using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories;
public class BaseRepository<T> where T: class,ISqlEntity
{
    protected readonly AppDbContext AppDbContext;
    protected BaseRepository(AppDbContext appDbContext)
    {
        AppDbContext = appDbContext;
    }
    //Create
    public async Task InsertAsync(T entity)
    {
        await AppDbContext.Set<T>().AddAsync(entity);
    }
     //Read
    public async Task<List<T>> GetAllAsync()
    {
        return await AppDbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await AppDbContext.Set<T>().FindAsync(id);
    }

    public async Task<T?> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await AppDbContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    public async Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate)
    {
        return await AppDbContext.Set<T>().Where(predicate).ToListAsync();
    }
    //Update 
    public async Task<bool> UpdateAsync(T entity,T newValue)
    {
       AppDbContext.Entry(entity).CurrentValues.SetValues(newValue);
       await AppDbContext.SaveChangesAsync();
       return true;
    
    }
    public async Task<bool> UpdateByIdAsync(int id,T newValue)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) 
        {
            await UpdateAsync(entity, newValue);
            return true;
        }
        else return false;
    }
    //Delete
    public async Task<bool> DeleteAsync(T entity)
    {
        AppDbContext.Set<T>().Remove(entity);
        await AppDbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null) 
        {
            await DeleteAsync(entity);
            return true;
        }
        else return false;
    }

    public async Task<int> WhereDeleteAsync(Expression<Func<T, bool>> predicate)
    {
        var entities = await AppDbContext.Set<T>().Where(predicate).ToListAsync();
        AppDbContext.Set<T>().RemoveRange(entities);
        return await AppDbContext.SaveChangesAsync();
    }
    //other common operations
    public async Task<bool> SaveChangesAsync() => await AppDbContext.SaveChangesAsync() > 0;
    
}