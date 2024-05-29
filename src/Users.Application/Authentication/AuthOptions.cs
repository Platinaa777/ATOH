namespace Users.Application.Authentication;

public class AuthOptions
{
    public bool ValidateIssuerKey { get; set; }
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateLifetime { get; set; }
    public int ClockSkew { get; set; }
    public string JwtKey { get; set; }
    public int ExpireMinutes { get; set; }
}