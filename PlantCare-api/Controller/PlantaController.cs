using Microsoft.AspNetCore.Mvc;
using PlantCare_api.Entity;
using PlantCare_api.Service;

namespace PlantCare_api.Controller;

[ApiController]
[Route("api/[controller]")]
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
    public async Task<ActionResult<IEnumerable<Planta>>> GetAll()
    {
        var plantas = await _plantaService.GetAllAsync();
        return Ok(plantas);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Planta>> GetById(int id)
    {
        var planta = await _plantaService.GetByIdAsync(id);
        
        if (planta == null)
        {
            return NotFound(); 
        }

        return Ok(planta);
    }
    [HttpPost]
    public async Task<ActionResult<Planta>> Create([FromBody] Planta planta)
    {
        _logger.LogInformation("Recebida requisição para criar nova planta: {@Planta}", planta);

        try
        {
            var novaPlanta = await _plantaService.CreateAsync(planta);
            
            _logger.LogInformation("Planta {PlantaId} criada com sucesso para o usuário {UsuarioId}", novaPlanta.Id, novaPlanta.UsuarioId);
            return CreatedAtAction(nameof(GetById), new { id = novaPlanta.Id }, novaPlanta);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro interno ao tentar criar a planta da espécie {Especie}", planta.Especie);
            return StatusCode(500, "Erro interno no servidor.");
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Planta planta)
    {
        _logger.LogInformation("Recebida requisição para atualizar a planta com ID {PlantaId}: {@Planta}", id, planta);

        if (id != planta.Id)
        {
            _logger.LogWarning("Falha de validação na atualização: ID da URL ({UrlId}) não coincide com o ID do corpo ({BodyId})", id, planta.Id);
            return BadRequest("The ID in the URL does not match the ID in the body.");
        }

        try
        {
            await _plantaService.UpdateAsync(id, planta);
            
            _logger.LogInformation("Planta {PlantaId} atualizada com sucesso", id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Tentativa de atualização falhou. Planta com ID {PlantaId} não foi encontrada", id);
            return NotFound(); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado ao tentar atualizar a planta {PlantaId}", id);
            throw;
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _plantaService.DeleteAsync(id);
            _logger.LogInformation("Planta {PlantaId} excluída com sucesso", id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            _logger.LogWarning("Tentativa de exclusão falhou. Planta com ID {PlantaId} não foi encontrada", id);
            return NotFound(); 
        }
    }
}