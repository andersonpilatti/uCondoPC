using Core.DTOs.Response;
using Domain.Model;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Data.Interfaces.Repositories;

public interface IPlanoContaRepository
{
    Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate);

    Task AddAsync(PlanoContaEntity entity);
    Task DeleteAsync(int id);

    Task <PlanoContaEntity?> GetByIdAsync(int id);
    Task<int> GetIdByContaAsync(string conta);

    Task<bool> AnyAsync(Expression<Func<PlanoContaEntity, bool>> match);

    Task<IEnumerable<PlanoContaPaiElegivelResponseDTO>> ListEligibleParentAccountsAsync();
    Task<string> SugestNewAccountCodeAsync(string? CodigoContaPai);
}
