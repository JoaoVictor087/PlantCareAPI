using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Moq;
using PlantCare.Application.Common.Hateoas;
using PlantCare.Application.DTOs;
using PlantCare.Application.Interfaces.Services;

namespace PlantCare_api.Tests.Integration.Controllers;

public class PlantaControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _factory;

    public PlantaControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((_, config) =>
            {
                config.AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:DefaultConnection"] = "Data Source=fake;User Id=fake;Password=fake;",
                    ["MongoDb:ConnectionString"] = "mongodb://localhost:27017",
                    ["MongoDb:DatabaseName"] = "PlantCareTest",
                    ["Jwt:Key"] = "PlantCare_SuperSecretKey_Min32Chars_2026!",
                    ["Jwt:Issuer"] = "PlantCareAPI",
                    ["Jwt:Audience"] = "PlantCareAPI"
                });
            });

            builder.ConfigureServices(services =>
            {
                services.RemoveAll<IPlantaService>();
                var mockService = new Mock<IPlantaService>();

                mockService.Setup(s => s.GetPagedAsync(It.IsAny<PlantaQuery>(), It.IsAny<string>()))
                    .ReturnsAsync(new PagedResource<PlantaDto>
                    {
                        Data = [],
                        Pagination = new PaginationMetadata { Page = 1, PageSize = 10, TotalItems = 0, TotalPages = 0 },
                        Links = []
                    });

                mockService.Setup(s => s.GetByIdAsync(99999, It.IsAny<string>()))
                    .ThrowsAsync(new PlantCare.Domain.Exceptions.NotFoundException("Planta não encontrada"));

                services.AddScoped(_ => mockService.Object);
            });
        });

        _client = _factory.CreateClient();
        _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {GerarTokenJwt()}");
    }

    [Fact]
    public async Task GetAll_QuandoAutenticado_RetornaStatusCode200()
    {
        var response = await _client.GetAsync("/api/planta?page=1&pageSize=10");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task GetById_IdInexistente_RetornaStatusCode404()
    {
        var response = await _client.GetAsync("/api/planta/99999");
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task GetAll_SemAutenticacao_RetornaStatusCode401()
    {
        var client = _factory.CreateClient();
        var response = await client.GetAsync("/api/planta");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    private static string GerarTokenJwt()
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("PlantCare_SuperSecretKey_Min32Chars_2026!"));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: "PlantCareAPI",
            audience: "PlantCareAPI",
            claims: new[] { new Claim(ClaimTypes.NameIdentifier, "1") },
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
