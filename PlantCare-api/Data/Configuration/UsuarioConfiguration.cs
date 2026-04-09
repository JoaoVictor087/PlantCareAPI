using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PlantCare_api.Entity;

namespace PlantCare_api.Data.Configuration;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("APP_USUARIOS");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Nome).IsRequired().HasMaxLength(100);
        builder.Property(u => u.Nome).IsRequired().HasMaxLength(150);
        builder.Property(u => u.Nome).IsRequired();
    }
}