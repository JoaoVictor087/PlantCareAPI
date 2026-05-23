using Microsoft.Extensions.DependencyInjection;
using PlantCare.Application.Interfaces.Services;
using PlantCare.Application.Services;

namespace PlantCare.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IPlantaService, PlantaService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRegistroCuidadoService, RegistroCuidadoService>();
        services.AddScoped<ILinkBuilderService, LinkBuilderService>();
        return services;
    }
}
