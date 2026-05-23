using PlantCare.Application.Common.Hateoas;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Application.Interfaces.Services;
using PlantCare.Application.Mappings;
using PlantCare.Domain.Entities;
using PlantCare.Domain.Exceptions;

namespace PlantCare.Application.Services;

public class RegistroCuidadoService : IRegistroCuidadoService
{
    private readonly IRegistroCuidadoRepository _repository;
    private readonly IPlantaRepository _plantaRepository;
    private readonly ILinkBuilderService _linkBuilder;

    public RegistroCuidadoService(
        IRegistroCuidadoRepository repository,
        IPlantaRepository plantaRepository,
        ILinkBuilderService linkBuilder)
    {
        _repository = repository;
        _plantaRepository = plantaRepository;
        _linkBuilder = linkBuilder;
    }

    public async Task<RegistroCuidadoDto> CreateAsync(CreateRegistroCuidadoDto dto)
    {
        if (!await _plantaRepository.ExistsAsync(dto.PlantaId))
            throw new NotFoundException($"Planta com ID {dto.PlantaId} não encontrada.");

        var registro = new RegistroCuidado
        {
            PlantaId = dto.PlantaId,
            UsuarioId = dto.UsuarioId,
            TipoCuidado = dto.TipoCuidado,
            Observacao = dto.Observacao,
            DataRegistro = DateTime.UtcNow
        };

        var created = await _repository.AddAsync(registro);
        return created.ToDto();
    }

    public async Task<Resource<IEnumerable<RegistroCuidadoDto>>> GetByPlantaIdAsync(int plantaId, string baseUrl)
    {
        if (!await _plantaRepository.ExistsAsync(plantaId))
            throw new NotFoundException($"Planta com ID {plantaId} não encontrada.");

        var registros = await _repository.GetByPlantaIdAsync(plantaId);
        var dtos = registros.Select(r => r.ToDto());

        return _linkBuilder.BuildResource(
            dtos,
            ("self", $"{baseUrl}/api/registroscuidado/planta/{plantaId}", "GET"),
            ("planta", $"{baseUrl}/api/planta/{plantaId}", "GET"));
    }
}
