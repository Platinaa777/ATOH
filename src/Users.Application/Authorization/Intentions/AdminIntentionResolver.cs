using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;

namespace Users.Application.Authorization.Intentions;

public class AdminIntentionResolver : IIntentionResolver<AdminIntention>
{
    public ValueTask<bool> IsAllowedAsync(
        Identity userIdentity,
        AdminIntention intention,
        CancellationToken ct)
    {
        return intention switch
        {
            AdminIntention.CreateAdmin => ValueTask.FromResult(userIdentity.IsAdmin), 
            _ => ValueTask.FromResult(false)
        };
    }
}