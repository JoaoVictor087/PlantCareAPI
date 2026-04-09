using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlantCare_api.Entity;

public class Usuario
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    [JsonIgnore]
    public ICollection<Planta> Plantas { get; set; } = new List<Planta>();
    
    
}