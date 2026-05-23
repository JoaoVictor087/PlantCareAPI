using Moq;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Application.Interfaces.Services;
using PlantCare.Application.Services;
using PlantCare.Domain.Entities;
using PlantCare.Domain.Exceptions;

namespace PlantCare_api.Tests.Unit.Service;

public class UsuarioServiceTests
{
    [Fact]
    public async Task CreateAsync_EmailDuplicado_DeveLancarBusinessRuleException()
    {
        var repository = new Mock<IUsuarioRepository>();
        var hasher = new Mock<IPasswordHasher>();
        repository.Setup(r => r.GetByEmailAsync("teste@fiap.com")).ReturnsAsync(new Usuario { Email = "teste@fiap.com" });

        var service = new UsuarioService(repository.Object, hasher.Object);
        var dto = new CreateUsuarioDto { Nome = "Teste", Email = "teste@fiap.com", Senha = "123456" };

        await Assert.ThrowsAsync<BusinessRuleException>(() => service.CreateAsync(dto));
    }
}
