using Domain.Model;
using System.Linq.Expressions;

namespace Services.Interfaces;

public interface IPlanoContaService
{
    Task AddAsync(PlanoContaEntity ent);
    Task DeleteAsync(int id);

    Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate);
}
