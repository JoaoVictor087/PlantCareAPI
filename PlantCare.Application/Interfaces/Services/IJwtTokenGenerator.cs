using PlantCare.Domain.Entities;

namespace PlantCare.Application.Interfaces.Services;

public interface IJwtTokenGenerator
{
    string GenerateToken(Usuario usuario, DateTime expiresAt);
}
