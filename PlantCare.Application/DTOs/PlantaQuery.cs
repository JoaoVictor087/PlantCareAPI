using PlantCare.Application.Common;

namespace PlantCare.Application.DTOs;

public class PlantaQuery : PaginationQuery
{
    public string? Nome { get; set; }
    public string? Especie { get; set; }
    public string? Status { get; set; }
    public int? UsuarioId { get; set; }
}
