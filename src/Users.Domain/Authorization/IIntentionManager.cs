namespace Users.Domain.Authorization;

public interface IIntentionManager
{
    ValueTask<bool> ResolveAsync<TIntention>(TIntention intention, CancellationToken ct) where TIntention : struct;
}