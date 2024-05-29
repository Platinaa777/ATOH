using Users.Domain.Authentication;

namespace Users.Domain.Authorization;


public interface IIntentionResolver
{

}

public interface IIntentionResolver<in TIntention> : IIntentionResolver
{
    ValueTask<bool> IsAllowedAsync(Identity userIdentity, TIntention intention, CancellationToken ct);
}
