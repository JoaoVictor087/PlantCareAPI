namespace PlantCare.Infrastructure.Auth;

public class JwtSettings
{
    public const string SectionName = "Jwt";
    public string Key { get; set; } = string.Empty;
    public string Issuer { get; set; } = "PlantCareAPI";
    public string Audience { get; set; } = "PlantCareAPI";
    public int ExpirationMinutes { get; set; } = 60;
}
