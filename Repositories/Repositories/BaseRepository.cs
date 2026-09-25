using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using shoppingapi2.Dtos.RequestDtos;
using shoppingapi2.Dtos.ResponseDtos;
using shoppingapi2.Models;

namespace shoppingapi2.Repositories.Repositories;

public class BaseRepository<T> where T : class, ISqlEntity
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
        await AppDbContext.SaveChangesAsync();
    }
    public async Task Insert(T entity)
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
    public void Update(T entity)
    {
        AppDbContext.Set<T>().Update(entity);
    }

    public async Task<bool> UpdateAsync(T entity, T newValue)
    {
        AppDbContext.Entry(entity).CurrentValues.SetValues(newValue);
        await AppDbContext.SaveChangesAsync();
        return true;

    }
    public async Task<bool> UpdateByIdAsync(int id, T newValue)
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
    public void Delete(T entity)
    {
        AppDbContext.Set<T>().Remove(entity);
    }
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

    public async Task<int> CountAsync(Expression<Func<T, bool>> predicate) =>
        await AppDbContext.Set<T>().CountAsync(predicate);

    public async Task<long> SumAsync(Expression<Func<T, bool>> predicate,Expression<Func<T, long>> selector) =>
        await AppDbContext.Set<T>().Where(predicate).SumAsync(selector);

     public string ToCamelCase(string s)
    {
        if (s.Length < 2) return s.ToLower();
        return char.ToUpper(s[0]) + s[1..];
    }
    public async Task<PaginateResponseDto<T>> Paginate(BaseFilterRequest filter,IQueryable<T> queryable)
    {
        var page = filter.Page is > 0 ? filter.Page : 1;
        var pageSize = filter.Pager > 0 ? filter.Pager : 12;

        if (!string.IsNullOrWhiteSpace(filter.OrderBy))
        {
            var items = filter.OrderBy.Split(":");
            var orderBy = items[0];
            var desc = items[1];

            Func<IQueryable<T>, IOrderedQueryable<T>> orderFunc =
                desc == "desc"
                    ? data => data.OrderByDescending(x => EF.Property<object>(x!, ToCamelCase(orderBy)))
                    : data => data.OrderBy(x => EF.Property<object>(x!, ToCamelCase(orderBy)));

            queryable = orderFunc(queryable);
        }
        else
        {
            queryable = queryable.OrderByDescending(x => EF.Property<object>(x!, ToCamelCase("Id")));
        }

        var res = await queryable
            .Skip((int)((page - 1) * pageSize))
            .Take((int)pageSize)
            .ToListAsync();

        var counts = 0;
        if (filter.Countable == true)
        {
            counts = await queryable.CountAsync();
        }

        return new PaginateResponseDto<T>()
        {
            Items = res,
            Page = page,
            Pages = (int)Math.Ceiling(counts / (float)pageSize),
            Pager = pageSize,
            Total = counts
        };
    }

}