using Microsoft.AspNetCore.Mvc;
using PlantCare_api.Entity;
using PlantCare_api.Repository;

namespace PlantCare_api.Controller;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioService;

    public UsuarioController(IUsuarioRepository usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Usuario usuario)
    {
        var novoUsuario = await _usuarioService.CreateAsync(usuario);
        return Ok(novoUsuario);
    }
}