using Microsoft.EntityFrameworkCore;
using PlantCare_api.Entity;

namespace PlantCare_api.Data;

public class PlantCareContext : DbContext
{
    public PlantCareContext(DbContextOptions<PlantCareContext> options) : base(options)
    {
    }
    
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Planta> Plantas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlantCareContext).Assembly);
    }
    
    
}