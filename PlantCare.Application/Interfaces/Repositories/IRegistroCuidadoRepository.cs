using PlantCare.Domain.Entities;

namespace PlantCare.Application.Interfaces.Repositories;

public interface IRegistroCuidadoRepository
{
    Task<RegistroCuidado> AddAsync(RegistroCuidado registro);
    Task<IEnumerable<RegistroCuidado>> GetByPlantaIdAsync(int plantaId);
    Task<RegistroCuidado?> GetByIdAsync(string id);
}
