using Users.Domain.Users;

namespace Users.Domain.Authentication;

public interface IAuthenticationService
{
    Identity AuthenticateIdentity(string token, CancellationToken ct = default);
    string GenerateAccessToken(User user);
}