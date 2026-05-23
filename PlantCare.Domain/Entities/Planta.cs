namespace PlantCare.Domain.Entities;

public class Planta
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Especie { get; set; } = string.Empty;
    public DateTime DataCadastro { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public string ImgLink { get; set; } = string.Empty;
    public double Umidade { get; set; }
    public double Temperatura { get; set; }
    public string Status { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public void Atualizar(string nome, string especie, string imgLink, double umidade, double temperatura, string status)
    {
        Nome = nome.Trim();
        Especie = especie;
        ImgLink = imgLink;
        Umidade = umidade;
        Temperatura = temperatura;
        Status = status;
        DataAtualizacao = DateTime.UtcNow;
    }

    public static Planta Criar(string nome, string especie, string imgLink, double umidade, double temperatura, string status, int usuarioId)
    {
        var agora = DateTime.UtcNow;
        return new Planta
        {
            Nome = nome.Trim(),
            Especie = especie,
            ImgLink = imgLink,
            Umidade = umidade,
            Temperatura = temperatura,
            Status = status,
            UsuarioId = usuarioId,
            DataCadastro = agora,
            DataAtualizacao = agora
        };
    }
}
