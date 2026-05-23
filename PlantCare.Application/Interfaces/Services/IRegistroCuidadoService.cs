using PlantCare.Application.Common.Hateoas;
using PlantCare.Application.DTOs;

namespace PlantCare.Application.Interfaces.Services;

public interface IRegistroCuidadoService
{
    Task<RegistroCuidadoDto> CreateAsync(CreateRegistroCuidadoDto dto);
    Task<Resource<IEnumerable<RegistroCuidadoDto>>> GetByPlantaIdAsync(int plantaId, string baseUrl);
}
