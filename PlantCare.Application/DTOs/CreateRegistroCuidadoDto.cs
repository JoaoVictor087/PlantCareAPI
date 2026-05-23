namespace PlantCare.Application.DTOs;

public class CreateRegistroCuidadoDto
{
    public int PlantaId { get; set; }
    public int UsuarioId { get; set; }
    public string TipoCuidado { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
}
