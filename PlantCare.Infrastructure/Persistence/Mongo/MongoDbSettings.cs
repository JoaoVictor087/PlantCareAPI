namespace PlantCare.Infrastructure.Persistence.Mongo;

public class MongoDbSettings
{
    public const string SectionName = "MongoDb";
    public string ConnectionString { get; set; } = "mongodb://localhost:27017";
    public string DatabaseName { get; set; } = "PlantCareDb";
    public string RegistrosCuidadoCollection { get; set; } = "registros_cuidado";
}
