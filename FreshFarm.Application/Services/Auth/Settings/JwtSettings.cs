using System.Security.Cryptography;

namespace FreshFarm.Application.Services.Auth.Settings;

public class JwtSettings
{
    public const string SectionName = "JwtSettings";
    private readonly Lazy<string> _secret;

    public string Secret => _secret.Value; // Accès thread-safe à la valeur générée
    public int ExpiryMinutes { get; init; }
    public string Issuer { get; init; }
    public string Audience { get; init; }

    public JwtSettings(int expiryMinutes, string issuer, string audience)
    {
        ExpiryMinutes = expiryMinutes;
        Issuer = issuer;
        Audience = audience;
        _secret = new Lazy<string>(GenerateSecret); // Initialisation paresseuse
    }

    private string GenerateSecret()
    {
        var secretKeyByteArray = new byte[32]; // 256 bit key
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(secretKeyByteArray);
        }
        return Convert.ToBase64String(secretKeyByteArray);
    }
}

