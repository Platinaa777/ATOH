namespace Users.Domain.Authentication;

public interface IAuthenticationService
{
    Identity AuthenticateIdentity(string token, CancellationToken ct = default);
}