using Microsoft.EntityFrameworkCore;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Domain.Entities;
using PlantCare.Infrastructure.Persistence.Oracle;

namespace PlantCare.Infrastructure.Persistence.Oracle.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly PlantCareContext _context;

    public UsuarioRepository(PlantCareContext context)
    {
        _context = context;
    }

    public async Task<Usuario> AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<Usuario?> GetByEmailAsync(string email) =>
        await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<Usuario?> GetByIdAsync(int id) =>
        await _context.Usuarios.FindAsync(id);
}
