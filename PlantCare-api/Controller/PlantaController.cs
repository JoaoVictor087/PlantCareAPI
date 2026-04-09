using Microsoft.AspNetCore.Mvc;
using PlantCare_api.Entity;
using PlantCare_api.Service;

namespace PlantCare_api.Controller;

[ApiController]
[Route("api/[controller]")]
public class PlantaController : ControllerBase
{
    private readonly IPlantaService _plantaService;

    public PlantaController(IPlantaService plantaService)
    {
        _plantaService = plantaService;
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
        var novaPlanta = await _plantaService.CreateAsync(planta);
        
        return CreatedAtAction(nameof(GetById), new { id = novaPlanta.Id }, novaPlanta);
    }

    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Planta planta)
    {
        if (id != planta.Id)
        {
            return BadRequest("The ID in the URL does not match the ID in the body."); // 400 Bad Request
        }

        try
        {
            await _plantaService.UpdateAsync(id, planta);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); 
        }

        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _plantaService.DeleteAsync(id);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(); 
        }

        return NoContent();
    }
}