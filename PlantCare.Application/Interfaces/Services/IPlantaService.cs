using PlantCare.Application.Common;
using PlantCare.Application.Common.Hateoas;
using PlantCare.Application.DTOs;

namespace PlantCare.Application.Interfaces.Services;

public interface IPlantaService
{
    Task<PagedResource<PlantaDto>> GetPagedAsync(PlantaQuery query, string baseUrl);
    Task<Resource<PlantaDto>> GetByIdAsync(int id, string baseUrl);
    Task<PlantaDto> CreateAsync(CreatePlantaDto dto);
    Task UpdateAsync(int id, UpdatePlantaDto dto);
    Task DeleteAsync(int id);
}
