namespace PlantCare.Application.DTOs;

public class RegistroCuidadoDto
{
    public string Id { get; set; } = string.Empty;
    public int PlantaId { get; set; }
    public int UsuarioId { get; set; }
    public string TipoCuidado { get; set; } = string.Empty;
    public string Observacao { get; set; } = string.Empty;
    public DateTime DataRegistro { get; set; }
}
