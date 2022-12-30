using Data.Context;
using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace PlanoConta.Test.Mocks;

internal class ConnectionMock
{
    public readonly AppDbContext DB;

    public ConnectionMock()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
            .Options;

        DB = new AppDbContext(options);

        if (DB.PlanoConta.Count() == 0)
        {
            InitializePlanoContaAddAsync();
            InitializeSugestNewAccountCodeAsync();
            InitializeDeleteAsync();
        }
    }

    private void InitializePlanoContaAddAsync()
    {
        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 1,
            Codigo = "1",
            IdPai = null,
            InAceitaLancamento = false,
            Nome = "Receita",
            Tipo = "R"
        });

        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 2,
            Codigo = "1.1",
            IdPai = 1,
            InAceitaLancamento = true,
            Nome = "Taxa condominal",
            Tipo = "R"
        });

        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 3,
            Codigo = "1.2",
            IdPai = 1,
            InAceitaLancamento = true,
            Nome = "Reserva de dependencia",
            Tipo = "R"
        });

        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 4,
            Codigo = "1.3",
            IdPai = 1,
            InAceitaLancamento = true,
            Nome = "Multas",
            Tipo = "R"
        });

        DB.SaveChanges();
    }

    private void InitializeSugestNewAccountCodeAsync()
    {
        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 100,
            Codigo = "9",
            IdPai = null,
            InAceitaLancamento = false,
            Nome = "Teste",
            Tipo = "R"
        });

        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 101,
            Codigo = "9.9",
            IdPai = 100,
            InAceitaLancamento = false,
            Nome = "Teste - nivel 1",
            Tipo = "R"
        });

        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 102,
            Codigo = "9.9.999",
            IdPai = 101,
            InAceitaLancamento = false,
            Nome = "Teste - nivel 2",
            Tipo = "R"
        });

        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 103,
            Codigo = "9.9.999.999",
            IdPai = 102,
            InAceitaLancamento = false,
            Nome = "Teste - nivel 3",
            Tipo = "R"
        });

        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 104,
            Codigo = "9.9.999.999.998",
            IdPai = 103,
            InAceitaLancamento = false,
            Nome = "Teste - nivel 4",
            Tipo = "R"
        });

        DB.Add<PlanoContaEntity>(new PlanoContaEntity
        {
            Id = 105,
            Codigo = "9.10",
            IdPai = 100,
            InAceitaLancamento = false,
            Nome = "Teste - nivel 1",
            Tipo = "R"
        });

        DB.SaveChanges();
    }

    private void InitializeDeleteAsync()
    {
        if (!DB.PlanoConta.Any(a => a.Id == 888))
        {
            DB.Add<PlanoContaEntity>(new PlanoContaEntity
            {
                Id = 888,
                Codigo = "8",
                IdPai = null,
                InAceitaLancamento = false,
                Nome = "Para remover",
                Tipo = "R"
            });

            DB.SaveChanges();
        }
        
    }
}