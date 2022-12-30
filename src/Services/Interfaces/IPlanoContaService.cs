using Core.DTOs.Base;
using Core.DTOs.Request;
using Core.DTOs.Response;
using Domain.Model;
using System.Linq.Expressions;

namespace Services.Interfaces;

public interface IPlanoContaService
{
    Task AddAsync(PlanoContaAddRequestDTO ent);
    Task DeleteAsync(string contaCodigo);

    Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate);

    Task<IEnumerable<PlanoContaResponseDTO>> ListEligibleParentAccountsAsync();
    Task<BaseListParametersResponseDTO<PlanoContaResponseDTO>> ListGridAsync(DataTableRequestDTO param);

    Task<string> SugestNewAccountCodeAsync(string? CodigoContaPai);
}
