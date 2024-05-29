using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Users.Application.Authentication;
using Users.Domain.Authentication;
using Users.Domain.Users;
using IAuthenticationService = Users.Domain.Authentication.IAuthenticationService;

namespace Users.Infrastructure.Authentication;

public class JwtAuthenticationService : IAuthenticationService
{
    private readonly AuthOptions _authOptions;

    public JwtAuthenticationService(
        AuthOptions authOptions)
    {
        _authOptions = authOptions;
    }
    
    public Identity AuthenticateIdentity(string token, CancellationToken ct = default)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = _authOptions.ValidateIssuerKey,
            ValidateAudience = _authOptions.ValidateAudience,
            ValidateIssuer = _authOptions.ValidateIssuer,
            ValidateLifetime = _authOptions.ValidateLifetime,
            ClockSkew = new TimeSpan(_authOptions.ClockSkew),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.JwtKey))
        };

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(
                token,
                tokenValidationParameters,
                out SecurityToken securityToken);

            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            if (principal is null)
                return Identity.CreateGuestIdentity();

            return ExtractIdentity(principal);
        }
        catch (Exception)
        {
            return Identity.CreateGuestIdentity();   
        }
    }

    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Login", user.Login),
            new Claim("IsAdmin", user.IsAdmin.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.JwtKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiration = DateTime.Now.AddMinutes(_authOptions.ExpireMinutes);
        // Create the JWT token
        var token = new JwtSecurityToken(
            claims: claims,
            expires: expiration,
            signingCredentials: credentials
        );

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }

    private Identity ExtractIdentity(ClaimsPrincipal principal)
    {
        var userId = ExtractClaimData(principal, "UserId");
        var login = ExtractClaimData(principal, "Login");
        var isAdmin = ExtractClaimData(principal, "IsAdmin");

        if (userId is null || login is null || isAdmin is null)
            return Identity.CreateGuestIdentity();

        var identity = new Identity(
            userId,
            login,
            isAdmin == "true");
        
        return identity;
    }

    private string? ExtractClaimData(ClaimsPrincipal principal, string claim)
    {
        var tokenClaim = principal.FindFirst(claim);
        return tokenClaim?.Value;
    }
}