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
                Id = 5,
                Codigo = "1.4",
                IdPai = 0,
                InAceitaLancamento = true,
                Nome = "Juros",
                Tipo = "R"
            };

            var exception = Assert.ThrowsAsync<PlanoContaExeception>(() => _service.AddAsync(entity));
            Assert.That(exception.ErrorCode, Is.EqualTo(1000));
        }

        [Test]
        public void ContaExistente()
        {
            entity = new PlanoContaEntity
            {
                Id = 5,
                Codigo = "1",
                InAceitaLancamento = true,
                Nome = "Juros",
                Tipo = "R"
            };

            var exception = Assert.ThrowsAsync<PlanoContaExeception>(() => _service.AddAsync(entity));
            Assert.That(exception.ErrorCode, Is.EqualTo(1004));
        }

        [Test]
        public void TiposDiferentesFamilia()
        {
            entity = new PlanoContaEntity
            {
                Id = 5,
                IdPai = 1,
                Codigo = "1.5",
                InAceitaLancamento = true,
                Nome = "Juros",
                Tipo = "D"
            };

            var exception = Assert.ThrowsAsync<PlanoContaExeception>(() => _service.AddAsync(entity));
            Assert.That(exception.ErrorCode, Is.EqualTo(1001));
        }

        [Test]
        public void TiposContaInvalido()
        {
            entity = new PlanoContaEntity
            {
                Id = 5,
                Codigo = "1.5",
                InAceitaLancamento = true,
                Nome = "Juros",
                Tipo = "Z"
            };

            var exception = Assert.ThrowsAsync<PlanoContaExeception>(() => _service.AddAsync(entity));
            Assert.That(exception.ErrorCode, Is.EqualTo(1006));
        }
    }
}