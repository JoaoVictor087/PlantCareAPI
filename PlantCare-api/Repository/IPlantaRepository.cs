using PlantCare_api.Entity;

namespace PlantCare_api.Repository;

public interface IPlantaRepository
{
    Task<IEnumerable<Planta>> GetAllAsync();
    Task<Planta?> GetByIdAsync(int id);
    Task<Planta> AddAsync(Planta planta);
    Task UpdateAsync(Planta planta);
    Task DeleteAsync(int id);
}