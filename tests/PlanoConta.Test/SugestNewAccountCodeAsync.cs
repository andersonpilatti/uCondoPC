using Core.DTOs.Request;
using Data.Base;
using Data.Interfaces.Repositories;
using Data.Repository;
using Domain.Model;
using PlanoConta.Test.Mocks;
using Services;
using Services.Interfaces;

namespace PlanoConta.Test;

public class SugestNewAccountCodeAsync
{
    private IPlanoContaRepository _repository;
    private IPlanoContaService _service;
    private ConnectionMock _conn;

    private PlanoContaAddRequestDTO entity { get; set; }

    [SetUp]
    public void Setup()
    {
        _conn = new ConnectionMock();

        _repository = new PlanoContaRepository(new BaseRepository<PlanoContaEntity>(_conn.DB));
        _service = new PlanoContaService(_repository);
    }

    [Test]
    public async Task SugestaoNovoCodigoContaNivel_N0()
    {
        var result = await _service.SugestNewAccountCodeAsync("");
        Assert.That(result, Is.EqualTo("10"));
    }

    [Test]
    public async Task SugestaoNovoCodigoContaNivel_N1()
    {
        var result = await _service.SugestNewAccountCodeAsync("9");
        Assert.That(result, Is.EqualTo("9.11"));
    }

    [Test]
    public async Task SugestaoNovoCodigoContaNivel_N2()
    {
        var result = await _service.SugestNewAccountCodeAsync("9.9");
        Assert.That(result, Is.EqualTo("9.11"));
    }

    [Test]
    public async Task SugestaoNovoCodigoContaNivel_N3()
    {
        var result = await _service.SugestNewAccountCodeAsync("9.9.999");
        Assert.That(result, Is.EqualTo("9.11"));
    }

    [Test]
    public async Task SugestaoNovoCodigoContaNivel_N4()
    {
        var result = await _service.SugestNewAccountCodeAsync("9.9.999.999");
        Assert.That(result, Is.EqualTo("9.9.999.999.999"));
    }
}
