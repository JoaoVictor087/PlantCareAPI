using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Oracle.EntityFrameworkCore.Metadata;
using PlantCare.Infrastructure.Persistence.Oracle;

#nullable disable

namespace PlantCare.Infrastructure.Migrations;

[DbContext(typeof(PlantCareContext))]
partial class PlantCareContextModelSnapshot : ModelSnapshot
{
    protected override void BuildModel(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasAnnotation("ProductVersion", "8.0.11")
            .HasAnnotation("Relational:MaxIdentifierLength", 128);

        OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

        modelBuilder.Entity("PlantCare.Domain.Entities.Planta", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("NUMBER(10)");
            OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
            b.Property<DateTime>("DataAtualizacao").HasColumnType("TIMESTAMP(7)");
            b.Property<DateTime>("DataCadastro").HasColumnType("TIMESTAMP(7)");
            b.Property<string>("Especie").IsRequired().HasMaxLength(100).HasColumnType("NVARCHAR2(100)");
            b.Property<string>("ImgLink").IsRequired().HasColumnType("NVARCHAR2(2000)");
            b.Property<string>("Nome").IsRequired().HasMaxLength(100).HasColumnType("NVARCHAR2(100)");
            b.Property<string>("Status").IsRequired().HasColumnType("NVARCHAR2(2000)");
            b.Property<double>("Temperatura").HasColumnType("BINARY_DOUBLE");
            b.Property<double>("Umidade").HasColumnType("BINARY_DOUBLE");
            b.Property<int>("UsuarioId").HasColumnType("NUMBER(10)");
            b.HasKey("Id");
            b.HasIndex("UsuarioId");
            b.ToTable("APP_PLANTAS", (string)null);
        });

        modelBuilder.Entity("PlantCare.Domain.Entities.Usuario", b =>
        {
            b.Property<int>("Id").ValueGeneratedOnAdd().HasColumnType("NUMBER(10)");
            OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));
            b.Property<string>("Email").IsRequired().HasColumnType("NVARCHAR2(2000)");
            b.Property<string>("Nome").IsRequired().HasMaxLength(150).HasColumnType("NVARCHAR2(150)");
            b.Property<string>("Senha").IsRequired().HasColumnType("NVARCHAR2(2000)");
            b.HasKey("Id");
            b.ToTable("APP_USUARIOS", (string)null);
        });

        modelBuilder.Entity("PlantCare.Domain.Entities.Planta", b =>
        {
            b.HasOne("PlantCare.Domain.Entities.Usuario", "Usuario")
                .WithMany("Plantas")
                .HasForeignKey("UsuarioId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            b.Navigation("Usuario");
        });

        modelBuilder.Entity("PlantCare.Domain.Entities.Usuario", b =>
        {
            b.Navigation("Plantas");
        });
    }
}
