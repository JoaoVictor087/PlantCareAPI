using PlantCare.Application.DTOs;

namespace PlantCare.Application.Interfaces.Services;

public interface IUsuarioService
{
    Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto);
}
