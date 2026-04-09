using PlantCare_api.Entity;

namespace PlantCare_api.Service;

public interface IPlantaService
{
    Task<IEnumerable<Planta>> GetAllAsync();
    Task<Planta?> GetByIdAsync(int id);
    Task<Planta> CreateAsync(Planta planta);
    Task UpdateAsync(int id, Planta planta);
    Task DeleteAsync(int id);
}