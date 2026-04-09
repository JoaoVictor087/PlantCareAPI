using PlantCare_api.Entity;

namespace PlantCare_api.Repository;

public interface IUsuarioRepository
{
    Task<Usuario> CreateAsync(Usuario usuario);
}