using Core.Base;
using System.Linq.Expressions;

namespace Core.Interfaces.Base;

public interface IBaseRepository<TEntity> 
    where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> GetById(int id);

    Task AddAssync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(int id);

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> match);
}
