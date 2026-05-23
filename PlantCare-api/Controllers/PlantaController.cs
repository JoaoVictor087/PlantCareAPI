using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Services;

namespace PlantCare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PlantaController : ControllerBase
{
    private readonly IPlantaService _plantaService;
    private readonly ILogger<PlantaController> _logger;

    public PlantaController(IPlantaService plantaService, ILogger<PlantaController> logger)
    {
        _plantaService = plantaService;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] PlantaQuery query)
    {
        _logger.LogInformation("Listando plantas - Página {Page}, Tamanho {PageSize}", query.Page, query.PageSize);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var result = await _plantaService.GetPagedAsync(query, baseUrl);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(int id)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var result = await _plantaService.GetByIdAsync(id, baseUrl);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreatePlantaDto dto)
    {
        _logger.LogInformation("Criando planta {Nome} para usuário {UsuarioId}", dto.Nome, dto.UsuarioId);
        var created = await _plantaService.CreateAsync(dto);
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        return Created($"{baseUrl}/api/planta/{created.Id}", created);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePlantaDto dto)
    {
        await _plantaService.UpdateAsync(id, dto);
        _logger.LogInformation("Planta {PlantaId} atualizada", id);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        await _plantaService.DeleteAsync(id);
        _logger.LogInformation("Planta {PlantaId} excluída", id);
        return NoContent();
    }
}
