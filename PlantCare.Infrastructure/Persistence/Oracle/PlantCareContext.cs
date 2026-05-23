using Microsoft.EntityFrameworkCore;
using PlantCare.Domain.Entities;
using PlantCare.Infrastructure.Persistence.Oracle.Configurations;

namespace PlantCare.Infrastructure.Persistence.Oracle;

public class PlantCareContext : DbContext
{
    public PlantCareContext(DbContextOptions<PlantCareContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Planta> Plantas => Set<Planta>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PlantCareContext).Assembly);
    }
}
