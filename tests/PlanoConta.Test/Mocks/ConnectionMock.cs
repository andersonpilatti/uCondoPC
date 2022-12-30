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

        DB =  new AppDbContext(options);
    }

    public void InitializePlanoConta()
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
}