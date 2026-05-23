using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Application.Interfaces.Services;
using PlantCare.Infrastructure.Auth;
using PlantCare.Infrastructure.Persistence.Mongo;
using PlantCare.Infrastructure.Persistence.Mongo.Repositories;
using PlantCare.Infrastructure.Persistence.Oracle;
using PlantCare.Infrastructure.Persistence.Oracle.Repositories;
using System.Text;

namespace PlantCare.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoDbSettings>(configuration.GetSection(MongoDbSettings.SectionName));
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

        services.AddDbContext<PlantCareContext>(options =>
            options.UseOracle(
                configuration.GetConnectionString("DefaultConnection"),
                oracle => oracle.MigrationsAssembly(typeof(PlantCareContext).Assembly.FullName)));

        services.AddSingleton<MongoDbContext>();

        services.AddScoped<IPlantaRepository, PlantaRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IRegistroCuidadoRepository, RegistroCuidadoRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>()
            ?? new JwtSettings();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                };
            });

        services.AddAuthorization();

        return services;
    }

    public static IHealthChecksBuilder AddInfrastructureHealthChecks(
        this IHealthChecksBuilder healthChecks,
        IConfiguration configuration)
    {
        healthChecks.AddOracle(
            configuration.GetConnectionString("DefaultConnection")!,
            name: "oracle-db");

        var mongoSettings = configuration.GetSection(MongoDbSettings.SectionName).Get<MongoDbSettings>();
        if (mongoSettings is not null)
        {
            healthChecks.AddMongoDb(
                mongoSettings.ConnectionString,
                name: "mongodb");
        }

        return healthChecks;
    }
}
