using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Application.Interfaces.Services;
using PlantCare.Domain.Exceptions;

namespace PlantCare.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private const int TokenExpirationMinutes = 60;

    public AuthService(
        IUsuarioRepository usuarioRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<TokenResponseDto> LoginAsync(LoginDto dto)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(dto.Email.Trim().ToLowerInvariant());
        if (usuario is null || !_passwordHasher.Verify(dto.Senha, usuario.Senha))
            throw new BusinessRuleException("Credenciais inválidas.");

        var expiresAt = DateTime.UtcNow.AddMinutes(TokenExpirationMinutes);
        var token = _jwtTokenGenerator.GenerateToken(usuario, expiresAt);

        return new TokenResponseDto
        {
            Token = token,
            ExpiresAt = expiresAt,
            TokenType = "Bearer"
        };
    }
}
