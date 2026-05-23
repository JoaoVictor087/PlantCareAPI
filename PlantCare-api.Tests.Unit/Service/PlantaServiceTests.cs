using Moq;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Application.Interfaces.Services;
using PlantCare.Application.Services;
using PlantCare.Domain.Entities;
using PlantCare.Domain.Exceptions;

namespace PlantCare_api.Tests.Unit.Service;

public class PlantaServiceTests
{
    private readonly Mock<IPlantaRepository> _repositoryMock;
    private readonly Mock<ILinkBuilderService> _linkBuilderMock;
    private readonly PlantaService _service;

    public PlantaServiceTests()
    {
        _repositoryMock = new Mock<IPlantaRepository>();
        _linkBuilderMock = new Mock<ILinkBuilderService>();
        _service = new PlantaService(_repositoryMock.Object, _linkBuilderMock.Object);
    }

    [Fact]
    public async Task CreateAsync_PlantaValida_DeveRetornarNomeSemEspacos()
    {
        var dto = new CreatePlantaDto { Nome = "  Samambaia  ", Especie = "Pteridophyta", UsuarioId = 1, Status = "Ativa" };
        var planta = Planta.Criar(dto.Nome, dto.Especie, "", 50, 25, dto.Status, dto.UsuarioId);
        planta.Id = 1;

        _repositoryMock.Setup(r => r.AddAsync(It.IsAny<Planta>())).ReturnsAsync(planta);

        var result = await _service.CreateAsync(dto);

        Assert.Equal("Samambaia", result.Nome);
        _repositoryMock.Verify(r => r.AddAsync(It.Is<Planta>(p => p.Nome == "Samambaia")), Times.Once);
    }

    [Fact]
    public async Task CreateAsync_NomeVazio_DeveLancarBusinessRuleException()
    {
        var dto = new CreatePlantaDto { Nome = "   ", Especie = "X", UsuarioId = 1 };

        await Assert.ThrowsAsync<BusinessRuleException>(() => _service.CreateAsync(dto));
    }

    [Fact]
    public async Task DeleteAsync_IdInexistente_DeveLancarNotFoundException()
    {
        _repositoryMock.Setup(r => r.ExistsAsync(99)).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _service.DeleteAsync(99));
    }

    [Fact]
    public async Task UpdateAsync_IdInexistente_DeveLancarNotFoundException()
    {
        _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync((Planta?)null);

        await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.UpdateAsync(1, new UpdatePlantaDto { Nome = "Teste" }));
    }
}
