using PlantCare.Domain.Entities;

namespace PlantCare_api.Tests.Unit.Domain;

public class PlantaTests
{
    [Fact]
    public void Criar_DeveDefinirDatasETrimarNome()
    {
        var planta = Planta.Criar("  Orquídea  ", "Orchidaceae", "img.png", 60, 22, "Saudável", 1);

        Assert.Equal("Orquídea", planta.Nome);
        Assert.Equal(1, planta.UsuarioId);
        Assert.True(planta.DataCadastro <= DateTime.UtcNow);
        Assert.Equal(planta.DataCadastro, planta.DataAtualizacao);
    }

    [Fact]
    public void Atualizar_DeveAtualizarDataAtualizacao()
    {
        var planta = Planta.Criar("Cacto", "Cactaceae", "", 30, 28, "Ativa", 1);
        var dataAnterior = planta.DataAtualizacao;

        Thread.Sleep(10);
        planta.Atualizar("Cacto Grande", "Cactaceae", "nova.png", 35, 27, "Ativa");

        Assert.Equal("Cacto Grande", planta.Nome);
        Assert.True(planta.DataAtualizacao >= dataAnterior);
    }
}
