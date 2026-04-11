using Microsoft.EntityFrameworkCore;
using PlantCare_api.Data;
using PlantCare_api.Repository;
using PlantCare_api.Service;
using Serilog;
using Serilog.Core;

Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();
builder.Services.AddControllers(); 

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PlantCareContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IPlantaRepository, PlantaRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPlantaService, PlantaService>();

builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddHealthChecks()
    .AddOracle(builder.Configuration.GetConnectionString("DefaultConnection"), name: "Oracle-DB");


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHealthChecks("/health");

app.Run();

public partial class Program
{
}