using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PlantCare.Domain.Entities;

namespace PlantCare.Infrastructure.Persistence.Mongo;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;
    private readonly MongoDbSettings _settings;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        _settings = settings.Value;
        var client = new MongoClient(_settings.ConnectionString);
        _database = client.GetDatabase(_settings.DatabaseName);
    }

    public IMongoCollection<RegistroCuidado> RegistrosCuidado =>
        _database.GetCollection<RegistroCuidado>(_settings.RegistrosCuidadoCollection);
}
