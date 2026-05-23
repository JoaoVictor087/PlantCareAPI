using PlantCare.Application.Common;
using PlantCare.Application.Common.Hateoas;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Repositories;
using PlantCare.Application.Interfaces.Services;
using PlantCare.Application.Mappings;
using PlantCare.Domain.Entities;
using PlantCare.Domain.Exceptions;

namespace PlantCare.Application.Services;

public class PlantaService : IPlantaService
{
    private readonly IPlantaRepository _repository;
    private readonly ILinkBuilderService _linkBuilder;

    public PlantaService(IPlantaRepository repository, ILinkBuilderService linkBuilder)
    {
        _repository = repository;
        _linkBuilder = linkBuilder;
    }

    public async Task<PagedResource<PlantaDto>> GetPagedAsync(PlantaQuery query, string baseUrl)
    {
        var paged = await _repository.GetPagedAsync(query);
        var dtos = paged.Items.Select(p => p.ToDto()).ToList();
        var pagedDtos = new PagedResult<PlantaDto>
        {
            Items = dtos,
            Page = paged.Page,
            PageSize = paged.PageSize,
            TotalItems = paged.TotalItems
        };
        return _linkBuilder.BuildPagedResource(pagedDtos, query, baseUrl);
    }

    public async Task<Resource<PlantaDto>> GetByIdAsync(int id, string baseUrl)
    {
        var planta = await _repository.GetByIdAsync(id);
        if (planta is null)
            throw new NotFoundException($"Planta com ID {id} não encontrada.");

        return _linkBuilder.BuildResource(
            planta.ToDto(),
            ("self", $"{baseUrl}/api/planta/{id}", "GET"),
            ("update", $"{baseUrl}/api/planta/{id}", "PUT"),
            ("delete", $"{baseUrl}/api/planta/{id}", "DELETE"),
            ("registros-cuidado", $"{baseUrl}/api/registroscuidado/planta/{id}", "GET"));
    }

    public async Task<PlantaDto> CreateAsync(CreatePlantaDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new BusinessRuleException("O nome da planta é obrigatório.");

        var planta = Planta.Criar(dto.Nome, dto.Especie, dto.ImgLink, dto.Umidade, dto.Temperatura, dto.Status, dto.UsuarioId);
        var created = await _repository.AddAsync(planta);
        return created.ToDto();
    }

    public async Task UpdateAsync(int id, UpdatePlantaDto dto)
    {
        var planta = await _repository.GetByIdAsync(id);
        if (planta is null)
            throw new NotFoundException($"Planta com ID {id} não encontrada.");

        planta.Atualizar(dto.Nome, dto.Especie, dto.ImgLink, dto.Umidade, dto.Temperatura, dto.Status);
        await _repository.UpdateAsync(planta);
    }

    public async Task DeleteAsync(int id)
    {
        if (!await _repository.ExistsAsync(id))
            throw new NotFoundException($"Planta com ID {id} não encontrada.");

        await _repository.DeleteAsync(id);
    }
}
