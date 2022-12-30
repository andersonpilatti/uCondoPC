using Core.DTOs.Request;
using Core.DTOs.Response;
using Domain.Model;
using System.Linq.Expressions;

namespace Services.Interfaces;

public interface IPlanoContaService
{
    Task AddAsync(PlanoContaAddRequestDTO ent);
    Task DeleteAsync(int id);

    Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate);

    Task<IEnumerable<PlanoContaPaiElegivelResponseDTO>> ListEligibleParentAccountsAsync();
    Task<string> SugestNewAccountCodeAsync(string? CodigoContaPai);
}
