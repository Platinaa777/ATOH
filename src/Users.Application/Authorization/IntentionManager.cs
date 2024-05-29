using Users.Domain.Authentication;
using Users.Domain.Authorization;

namespace Users.Application.Authorization;

public class IntentionManager : IIntentionManager
{
    private readonly IEnumerable<IIntentionResolver> _resolvers;
    private readonly IIdentityProvider _identityProvider;

    public IntentionManager(
        IEnumerable<IIntentionResolver> resolvers,
        IIdentityProvider identityProvider)
    {
        _resolvers = resolvers;
        _identityProvider = identityProvider;
    }


    public async ValueTask<bool> ResolveAsync<TIntention>(TIntention intention, CancellationToken ct) where TIntention : struct
    {
        // Находим подходящего так скажем `разрешителя`
        var appropriateResolver = _resolvers
            .OfType<IIntentionResolver<TIntention>>()
            .FirstOrDefault();

        if (appropriateResolver is null)
            return true;

        return await appropriateResolver.IsAllowedAsync(_identityProvider.CurrentIdentity, intention, ct);
    }
}