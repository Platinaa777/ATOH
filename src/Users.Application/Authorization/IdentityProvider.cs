using Users.Domain.Authentication;

namespace Users.Application.Authorization;

public class IdentityProvider : IIdentityProvider
{
    public Identity CurrentIdentity { get; set; } = Identity.CreateGuestIdentity();
}