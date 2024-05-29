namespace Users.Domain.Authentication;

public interface IIdentityProvider
{
    Identity CurrentIdentity { get; set; }
}