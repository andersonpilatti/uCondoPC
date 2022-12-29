using Core.Base;
using Core.Interfaces.Base;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Base;

public class BaseRepository<TEntity>
    : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly AppDbContext _db;
    protected readonly DbSet<TEntity> _dbSet;

    public BaseRepository(AppDbContext db)
    {
        _db = db;
        _dbSet = _db.Set<TEntity>();
    }

    public async Task AddAssync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> match)
    {
        var result = await _dbSet.AnyAsync(match);
        return result;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = _dbSet.First(x => x.Id == id);

        _db.Remove(entity);
        await _db.SaveChangesAsync();
    }

    public async Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _dbSet.AsQueryable();

        if (predicate != null)
            query = query
                .Where(predicate)
                .AsNoTracking();

        return await query.ToListAsync();
    }

    public async Task<TEntity?> GetById(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await _db.SaveChangesAsync();
    }
}
