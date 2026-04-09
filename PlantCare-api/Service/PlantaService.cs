using PlantCare_api.Entity;
using PlantCare_api.Repository;

namespace PlantCare_api.Service;

public class PlantaService : IPlantaService
{
    private readonly IPlantaRepository _repository;

    public PlantaService(IPlantaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Planta>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Planta?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Planta> CreateAsync(Planta planta)
    {
        planta.Nome = planta.Nome.Trim();
        return await _repository.AddAsync(planta);
    }

    public async Task UpdateAsync(int id, Planta planta)
    {
        var existingPlanta = await _repository.GetByIdAsync(id);
        if (existingPlanta == null)
        {
            throw new KeyNotFoundException($"Planta with ID {id} was not found.");
        }
        
        existingPlanta.Nome = planta.Nome.Trim();
        existingPlanta.Especie = planta.Especie;
        
        await _repository.UpdateAsync(existingPlanta);
    }

    public async Task DeleteAsync(int id)
    {
        var existingPlanta = await _repository.GetByIdAsync(id);
        if (existingPlanta == null)
        {
            throw new KeyNotFoundException($"Planta with ID {id} was not found.");
        }

        await _repository.DeleteAsync(id);
    }
}