using Core.DTOs.Request;
using Core.Exceptions;
using Data.Base;
using Data.Interfaces.Repositories;
using Data.Repository;
using Domain.Model;
using PlanoConta.Test.Mocks;
using Services;
using Services.Interfaces;

namespace PlanoConta.Test;

public class DeleteAsync
{
    private IPlanoContaRepository _repository;
    private IPlanoContaService _service;
    private ConnectionMock _conn;

    [SetUp]
    public void Setup()
    {
        _conn = new ConnectionMock();

        _repository = new PlanoContaRepository(new BaseRepository<PlanoContaEntity>(_conn.DB));
        _service = new PlanoContaService(_repository);

    }

    [Test]
    public async Task ContaRemovida()
    {
        await _service.DeleteAsync("8"); 
    }

    [Test]
    public void ContaNaoLocalizada()
    {
        var exception = Assert.ThrowsAsync<PlanoContaExeception>(() => _service.DeleteAsync("8.8"));
        Assert.That(exception.ErrorCode, Is.EqualTo(1007));
    }
}
