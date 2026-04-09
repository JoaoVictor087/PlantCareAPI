using PlantCare_api.Data;
using PlantCare_api.Entity;

namespace PlantCare_api.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly PlantCareContext _context;

    public UsuarioRepository(PlantCareContext context)
    {
        _context = context;
    }

    public async Task<Usuario> CreateAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }
}