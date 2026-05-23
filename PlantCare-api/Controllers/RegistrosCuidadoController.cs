using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Services;

namespace PlantCare_api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RegistrosCuidadoController : ControllerBase
{
    private readonly IRegistroCuidadoService _service;

    public RegistrosCuidadoController(IRegistroCuidadoService service)
    {
        _service = service;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateRegistroCuidadoDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetByPlanta), new { plantaId = created.PlantaId }, created);
    }

    [HttpGet("planta/{plantaId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByPlanta(int plantaId)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var result = await _service.GetByPlantaIdAsync(plantaId, baseUrl);
        return Ok(result);
    }
}
