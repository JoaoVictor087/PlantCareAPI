using PlantCare.Application.DTOs;

namespace PlantCare.Application.Interfaces.Services;

public interface IAuthService
{
    Task<TokenResponseDto> LoginAsync(LoginDto dto);
}
