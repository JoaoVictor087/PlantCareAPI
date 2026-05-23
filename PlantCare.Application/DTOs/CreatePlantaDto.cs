namespace PlantCare.Application.DTOs;

public class CreatePlantaDto
{
    public string Nome { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public string ImgLink { get; set; } = string.Empty;
    public double Umidade { get; set; }
    public double Temperatura { get; set; }
    public string Status { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
}
