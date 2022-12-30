using Core.DTOs.Base;
using Core.DTOs.Request;
using Core.DTOs.Response;
using Core.Exceptions;
using Data.Interfaces.Repositories;
using Domain.Model;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using System.Linq.Expressions;

namespace Services;

public class PlanoContaService
    : IPlanoContaService
{
    private readonly IPlanoContaRepository _repository;

    public PlanoContaService(IPlanoContaRepository repository)
    {
        _repository = repository;
    }

    public async Task AddAsync(PlanoContaAddRequestDTO param)
    {
        var entity = await Mapper(param);

        if (entity.Tipo != "D" && entity.Tipo != "R")
        {
            throw new PlanoContaExeception(1006, "Tipo de conta inválido");
        }

        if (entity.IdPai != null)
        {
            var existe = await _repository.AnyAsync(a => a.Id == entity.IdPai);

            if (! existe)
            {
                throw new PlanoContaExeception(1000, "A conta Pai informada não existe");
            }


            if (await _repository.AnyAsync(a => a.Id == entity.IdPai && a.Tipo != entity.Tipo))
            {
                throw new PlanoContaExeception(1001, "A conta que esta sendo criada deve ser do mesmo tipo da conta Pai");
            }

            if (entity.InAceitaLancamento)
            {
                if (await _repository.AnyAsync(a => a.Id == entity.IdPai && a.InAceitaLancamento == true))
                {
                    throw new PlanoContaExeception(1002, "A conta Pai aceita receber lancamentos e não pode ter contas filhas por esse motivo.");
                }
            }
        }        

        if (await _repository.AnyAsync(a => a.Codigo == entity.Codigo))
        {
            throw new PlanoContaExeception(1004, "Código de conta já cadastrado");
        }

        var codigo = entity.Codigo.Split('.');

        foreach(var nivel in codigo) 
        {
            int valor = 0;
            
            if (! int.TryParse(nivel, out valor))
            {
                throw new PlanoContaExeception(1009, "Os níveis dos códigos informados devem ser numéricos");
            }

            if (valor > 999)
            {
                throw new PlanoContaExeception(1008, "O maior valor para um nível permitido é 999");
            }
        }

        await _repository.AddAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        if (await _repository.AnyAsync(a => a.Id == id))
        {
            throw new PlanoContaExeception(1007, "Conta não localizada");
        }

        await _repository.DeleteAsync(id);
    }

    public async Task<IEnumerable<PlanoContaResponseDTO>> ListEligibleParentAccountsAsync()
    {
        return await _repository.ListEligibleParentAccountsAsync();
    }

    public async Task<IEnumerable<PlanoContaEntity>> ListAsync(Expression<Func<PlanoContaEntity, bool>> predicate)
    {
        var result = await _repository.ListAsync(predicate);
        return result;
    }

    public async Task<PlanoContaEntity> Mapper(PlanoContaAddRequestDTO param)
    {
        var result = new PlanoContaEntity
        {
            Codigo = param.CodigoConta,
            Nome = param.Nome,
            Tipo = param.Tipo,
            InAceitaLancamento = param.InAceitaLancamento
        };

        if (!param.CodigoContaPai.IsNullOrEmpty())
        {
            result.IdPai = await _repository.GetIdByContaAsync(param.CodigoContaPai);
        }

        return result;
    }

    public async Task<string> SugestNewAccountCodeAsync(string? CodigoContaPai)
    {
        return await _repository.SugestNewAccountCodeAsync(CodigoContaPai);
    }

    public async Task<BaseListParametersResponseDTO<PlanoContaResponseDTO>> ListGridAsync(DataTableRequestDTO param)
    {
        return await _repository.ListGridAsync(param);
    }
}
