using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Application.Interfaces.Services;
using PlantCare.Application.Mappings;
using PlantCare.Domain.Entities;
using PlantCare.Domain.Exceptions;

namespace PlantCare.Application.Services;

public class UsuarioService : IUsuarioService
{
    private readonly IUsuarioRepository _repository;
    private readonly IPasswordHasher _passwordHasher;

    public UsuarioService(IUsuarioRepository repository, IPasswordHasher passwordHasher)
    {
        _repository = repository;
        _passwordHasher = passwordHasher;
    }

    public async Task<UsuarioDto> CreateAsync(CreateUsuarioDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Senha))
            throw new BusinessRuleException("E-mail e senha são obrigatórios.");

        var existente = await _repository.GetByEmailAsync(dto.Email);
        if (existente is not null)
            throw new BusinessRuleException("E-mail já cadastrado.");

        var usuario = new Usuario
        {
            Nome = dto.Nome.Trim(),
            Email = dto.Email.Trim().ToLowerInvariant(),
            Senha = _passwordHasher.Hash(dto.Senha)
        };

        var created = await _repository.AddAsync(usuario);
        return created.ToDto();
    }
}
