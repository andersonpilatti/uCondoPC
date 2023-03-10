using Core.Base;
using Data.Context;
using System.Linq.Expressions;

namespace Data.Interfaces.Base;

public interface IBaseRepository<TEntity> 
    where TEntity : BaseEntity
{
    AppDbContext DB { get; }

    Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetByIdAsync(int id);

    Task AddAssync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> match);
}
