using PlantCare.Application.DTOs;
using PlantCare.Domain.Entities;

namespace PlantCare.Application.Mappings;

public static class EntityMapper
{
    public static PlantaDto ToDto(this Planta planta) => new()
    {
        Id = planta.Id,
        Nome = planta.Nome,
        Especie = planta.Especie,
        DataCadastro = planta.DataCadastro,
        DataAtualizacao = planta.DataAtualizacao,
        ImgLink = planta.ImgLink,
        Umidade = planta.Umidade,
        Temperatura = planta.Temperatura,
        Status = planta.Status,
        UsuarioId = planta.UsuarioId
    };

    public static UsuarioDto ToDto(this Usuario usuario) => new()
    {
        Id = usuario.Id,
        Nome = usuario.Nome,
        Email = usuario.Email
    };

    public static RegistroCuidadoDto ToDto(this RegistroCuidado registro) => new()
    {
        Id = registro.Id,
        PlantaId = registro.PlantaId,
        UsuarioId = registro.UsuarioId,
        TipoCuidado = registro.TipoCuidado,
        Observacao = registro.Observacao,
        DataRegistro = registro.DataRegistro
    };
}
