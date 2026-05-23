using PlantCare.Application.Common;
using PlantCare.Application.DTOs;
using PlantCare.Domain.Entities;

namespace PlantCare.Application.Interfaces.Repositories;

public interface IPlantaRepository
{
    Task<PagedResult<Planta>> GetPagedAsync(PlantaQuery query);
    Task<Planta?> GetByIdAsync(int id);
    Task<Planta> AddAsync(Planta planta);
    Task UpdateAsync(Planta planta);
    Task DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}
