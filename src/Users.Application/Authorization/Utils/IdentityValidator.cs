using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;

namespace Users.Application.Authorization.Utils;

public static class IdentityValidator
{
    public static async Task ValidateIsAllowedToChangeOrThrowAsync(
        IIdentityProvider identityProvider,
        IIntentionManager intentionManager,
        string login,
        CancellationToken ct)
    {
        // обычные пользователи не могут менять информацию других пользователей
        var identity = identityProvider.CurrentIdentity;
        if (!identity.IsAdmin && identity.Login != login)
            throw new IntentionException();
        
        var isAllowed = await intentionManager
            .ResolveAsync(UserIntention.ChangeUserInfo, ct);

        if (!isAllowed)
            throw new IntentionException();
    }
}