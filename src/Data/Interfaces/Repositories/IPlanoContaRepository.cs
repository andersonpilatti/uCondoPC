using Core.DTOs.Base;
using Core.DTOs.Request;
using Core.DTOs.Response;
using Domain.Model;
using System.Linq.Expressions;

namespace Data.Interfaces.Repositories;

public interface IPlanoContaRepository
{
    Task AddAsync(PlanoContaEntity entity);
    Task DeleteAsync(int id);

    Task <PlanoContaEntity?> GetByIdAsync(int id);
    Task<int> GetIdByContaAsync(string conta);

    Task<bool> AnyAsync(Expression<Func<PlanoContaEntity, bool>> match);


    Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate);

    Task<IEnumerable<PlanoContaResponseDTO>> ListEligibleParentAccountsAsync();
    Task<BaseListParametersResponseDTO<PlanoContaResponseDTO>> ListGridAsync(DataTableRequestDTO param);

    Task<string> SugestNewAccountCodeAsync(string? CodigoContaPai);    
}
