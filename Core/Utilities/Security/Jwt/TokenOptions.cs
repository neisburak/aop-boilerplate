namespace Core.Utilities.Security.Jwt;

public class TokenOptions
{
    public string Audience { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public int Expiration { get; set; }
    public string SecurityKey { get; set; } = default!;
}