using Core.Base;
using System.Linq.Expressions;

namespace Core.Interfaces.Base;

public interface IBaseService<TEntity> 
    where TEntity : BaseEntity
{
    Task<string> AddAsync(TEntity entity);
    Task<string> UpdateAsync(TEntity entity);
    Task<string> DeleteAsync(int id);

    Task<IEnumerable<TEntity>> ListAsync(Expression<Func<TEntity, bool>> predicate);
}
