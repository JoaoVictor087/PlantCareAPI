using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlantCare_api.Entity;

public class Planta
{
    [Key]
    public int Id { get; set; }
    
    public String Nome { get; set; }
    
    public String Especie { get; set; }
    
    public DateTime DataCadastro { get; set; }
    
    public DateTime DataAtualizacao { get; set; }
    
    public String ImgLink { get; set; }
    
    public Double Umidade { get; set; }
    
    public Double Temperatura { get; set; }
    
    public String Status { get; set; }
    
    public int UsuarioId { get; set; }
    
    [JsonIgnore]
    public Usuario? Usuario { get; set; }
}