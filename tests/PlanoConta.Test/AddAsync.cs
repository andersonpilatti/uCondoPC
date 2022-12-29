using Core.Exceptions;
using Data.Base;
using Data.Interfaces.Repositories;
using Data.Repository;
using Domain.Model;
using PlanoConta.Test.Mocks;
using Services;
using Services.Interfaces;

namespace PlanoConta.Test
{
    public class AddAsync
    {
        private IPlanoContaRepository _repository;
        private IPlanoContaService _service;
        private ConnectionMock _conn;

        private PlanoContaEntity entity { get; set; }

        [SetUp]
        public void Setup()
        {
            _conn = new ConnectionMock();
            _conn.InitializePlanoConta();

            _repository = new PlanoContaRepository(new BaseRepository<PlanoContaEntity>(_conn.DB));
            _service = new PlanoContaService(_repository);
        }

        [Test]
        public void ContaPaiInexistente()
        {
            entity = new PlanoContaEntity
            {
                Codigo = "1.4",
                IdPai = 0,
                InAceitaLancamento = true,
                Nome = "Juros",
                Tipo = "R"
            };

            Assert.ThrowsAsync<PlanoContaExeception>(() => _service.AddAsync(entity));
        }
    }
}