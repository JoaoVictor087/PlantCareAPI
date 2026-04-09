using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantCare_api.Entity;

namespace PlantCare_api.Data.Configuration;

public class PlantaConfiguration : IEntityTypeConfiguration<Planta>
{
    public void Configure(EntityTypeBuilder<Planta> builder)
    {
        builder.ToTable("APP_PLANTAS");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Especie).HasMaxLength(100);

        builder.HasOne(p => p.Usuario)
            .WithMany(u => u.Plantas)
            .HasForeignKey(p => p.UsuarioId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}