using Xunit;
using Moq;
using PlantCare_api.Service;
using PlantCare_api.Repository;
using PlantCare_api.Entity;

namespace PlantCare_api.Tests.Unit.Service;

public class PlantaServiceTests
{
    private readonly Mock<IPlantaRepository> _plantaRepositoryMock;
    private readonly PlantaService _plantaService;

    public PlantaServiceTests()
    {
        _plantaRepositoryMock = new Mock<IPlantaRepository>();
        _plantaService = new PlantaService(_plantaRepositoryMock.Object);
    }
    
    [Fact]
    public async Task CreateAsync_PlantaValida_DeveRetornarPlantaComNomeSemEspacos()
    {
        var novaPlanta = new Planta { Nome = "  Samambaia  ", Especie = "Pteridophyta", UsuarioId = 1 };
        var plantaEsperada = new Planta { Id = 1, Nome = "Samambaia", Especie = "Pteridophyta", UsuarioId = 1 };
        
        _plantaRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Planta>()))
                             .ReturnsAsync(plantaEsperada);
        
        var result = await _plantaService.CreateAsync(novaPlanta);
        
        Assert.NotNull(result);
        Assert.Equal("Samambaia", result.Nome);
        
        _plantaRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Planta>()), Times.Once);
    }

    [Fact]
    public async Task GetByIdAsync_IdExistente_DeveRetornarPlanta()
    {
        
        var plantaEsperada = new Planta { Id = 1, Nome = "Cacto", UsuarioId = 1 };
        _plantaRepositoryMock.Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(plantaEsperada);
        
        var result = await _plantaService.GetByIdAsync(1);
        
        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }
}