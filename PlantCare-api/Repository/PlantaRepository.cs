using Microsoft.EntityFrameworkCore;
using PlantCare_api.Data;
using PlantCare_api.Entity;

namespace PlantCare_api.Repository;

public class PlantaRepository : IPlantaRepository
{
    private readonly PlantCareContext _context;
    
    public PlantaRepository(PlantCareContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Planta>> GetAllAsync()
    {
        return await _context.Plantas
            .Include(p => p.Usuario) 
            .ToListAsync();
    }

    public async Task<Planta?> GetByIdAsync(int id)
    {
        return await _context.Plantas
            .Include(p => p.Usuario)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Planta> AddAsync(Planta planta)
    {
        await _context.Plantas.AddAsync(planta);
        await _context.SaveChangesAsync();
        return planta;
    }

    public async Task UpdateAsync(Planta planta)
    {
        _context.Plantas.Update(planta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var planta = await _context.Plantas.FindAsync(id);
        if (planta != null)
        {
            _context.Plantas.Remove(planta);
            await _context.SaveChangesAsync();
        }
    }
}