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
            AdminIntention.CreateAdmin 
                or AdminIntention.DeleteUser
                or AdminIntention.RecoverUser
                or AdminIntention.GetActiveUsers
                or AdminIntention.GetUserForAdmin => ValueTask.FromResult(userIdentity.IsAdmin), 
            _ => ValueTask.FromResult(false)
        };
    }
}