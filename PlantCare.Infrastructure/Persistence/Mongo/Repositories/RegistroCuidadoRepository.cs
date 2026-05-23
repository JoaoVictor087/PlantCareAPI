using MongoDB.Bson;
using MongoDB.Driver;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Domain.Entities;

namespace PlantCare.Infrastructure.Persistence.Mongo.Repositories;

public class RegistroCuidadoRepository : IRegistroCuidadoRepository
{
    private readonly MongoDbContext _context;

    public RegistroCuidadoRepository(MongoDbContext context)
    {
        _context = context;
    }

    public async Task<RegistroCuidado> AddAsync(RegistroCuidado registro)
    {
        if (string.IsNullOrEmpty(registro.Id))
            registro.Id = ObjectId.GenerateNewId().ToString();

        await _context.RegistrosCuidado.InsertOneAsync(registro);
        return registro;
    }

    public async Task<IEnumerable<RegistroCuidado>> GetByPlantaIdAsync(int plantaId) =>
        await _context.RegistrosCuidado
            .Find(r => r.PlantaId == plantaId)
            .SortByDescending(r => r.DataRegistro)
            .ToListAsync();

    public async Task<RegistroCuidado?> GetByIdAsync(string id) =>
        await _context.RegistrosCuidado
            .Find(r => r.Id == id)
            .FirstOrDefaultAsync();
}
