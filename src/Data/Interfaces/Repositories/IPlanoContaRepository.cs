using Domain.Model;
using System.Linq.Expressions;

namespace Data.Interfaces.Repositories;

public interface IPlanoContaRepository
{
    Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate);
    
    Task AddAsync(PlanoContaEntity entity);
    Task DeleteAsync(int id);

    Task<bool> AnyAsync(Expression<Func<PlanoContaEntity, bool>> match);
}
