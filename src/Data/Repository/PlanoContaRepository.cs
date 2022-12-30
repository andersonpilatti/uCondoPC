using Core.DTOs.Response;
using Data.Interfaces.Base;
using Data.Interfaces.Repositories;
using Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;
using System.Reflection.Metadata;

namespace Data.Repository
{
    public class PlanoContaRepository
        : IPlanoContaRepository
    {
        private readonly IBaseRepository<PlanoContaEntity> _baseRepository;

        public PlanoContaRepository(IBaseRepository<PlanoContaEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        public async Task AddAsync(PlanoContaEntity entity)
        {
            await _baseRepository.AddAssync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<PlanoContaEntity, bool>> match)
        {
            return await _baseRepository.AnyAsync(match);
        }

        public async Task DeleteAsync(int id)
        {
            await _baseRepository.DeleteAsync(id);
        }

        public async Task<PlanoContaEntity?> GetByIdAsync(int id)
        {
            return await _baseRepository.GetByIdAsync(id);
        }

        public async Task<int> GetIdByContaAsync(string conta)
        {
            return 
                await _baseRepository.DB
                        .PlanoConta
                            .Where(w => w.Codigo == conta)
                            .Select(s => s.Id)
                            .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PlanoContaPaiElegivelResponseDTO>> ListEligibleParentAccountsAsync()
        {
            return
                await _baseRepository.DB
                        .PlanoConta
                            .Where(w => w.InAceitaLancamento == false)
                            .Select(s => new PlanoContaPaiElegivelResponseDTO
                            {
                                Codigo = s.Codigo,
                                Nome = s.Nome,
                                Tipo = s.Tipo,
                            })
                            .OrderBy(o => o.Codigo)
                            .ToListAsync();
        }

        public async Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate)
        {
            return await _baseRepository.ListAsync(predicate);
        }

        public async Task<string> SugestNewAccountCodeAsync(string? CodigoContaPai)
        {
            var result = string.Empty;

            if (CodigoContaPai.IsNullOrEmpty())
            {
                var qr =
                    await _baseRepository.DB
                            .PlanoConta
                                .Where(w => w.IdPai == null)
                                .MaxAsync(m => m.Codigo);

                if (qr == null)
                {
                    result = "1";
                }
                else
                {
                    result = (Convert.ToInt16(qr) + 1).ToString();
                }
            }
            else
            {
                var qr =
                    await _baseRepository.DB
                            .PlanoConta
                                .Where(w => w.ContaPai.Codigo == CodigoContaPai)
                                .MaxAsync(m => m.Codigo);

                var niveis = qr.Split('.');

                if (niveis[niveis.Length -1] == "999")
                {
                    var novoNivel = CodigoContaPai.Split(".");
                    var novoPai = novoNivel.Take(novoNivel.Length - 1);

                    result = await SugestNewAccountCodeAsync(string.Join(".", novoPai));
                }
                else
                {
                    var codigo = Convert.ToInt16(niveis[niveis.Length - 1]);
                    var posicao = niveis.Length - 1;

                    string novoCodigo;

                    do
                    {
                        novoCodigo = string.Empty;

                        codigo++;
                        niveis[posicao] = codigo.ToString();

                        novoCodigo = string.Join(".", niveis);
                    } while (await _baseRepository
                                .DB.PlanoConta.AnyAsync(a => a.Codigo == novoCodigo));
                    
                    result = novoCodigo;
                }                
            }            

            return result;
        }
    }
}