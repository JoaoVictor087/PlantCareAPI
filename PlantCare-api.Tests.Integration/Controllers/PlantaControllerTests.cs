using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;
using Moq;
using PlantCare_api.Entity;
using PlantCare_api.Service;


namespace PlantCare_api.Tests.Integration.Controllers;

public class PlantaControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public PlantaControllerTests(WebApplicationFactory<Program> factory)
    {
        var customFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var inMemorySettings = new Dictionary<string, string?>
                {
                    {"ConnectionStrings:DefaultConnection", "Data Source=fake;User Id=fake;Password=fake;"},
                    {"ApplicationInsights:ConnectionString", "InstrumentationKey=00000000-0000-0000-0000-000000000000;IngestionEndpoint=https://fake.in.applicationinsights.azure.com/"}
                };
                config.AddInMemoryCollection(inMemorySettings);
            });
            
            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IPlantaService>(); 
                
                var mockService = new Mock<IPlantaService>();
                
                mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(new List<Planta>());
                
                services.AddScoped<IPlantaService>(_ => mockService.Object);
            });
        });

        _client = customFactory.CreateClient();
    }

    [Fact]
    public async Task GetAll_QuandoChamado_RetornaStatusCode200()
    {
        var response = await _client.GetAsync("/api/planta");
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_IdInexistente_RetornaStatusCode404NotFound()
    {
        var response = await _client.GetAsync("/api/planta/99999");
        
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
}