using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Users.Repos;

namespace Users.Application.Authorization.Intentions;

public class UserIntentionResolver : IIntentionResolver<UserIntention>
{
    private readonly IUserSearchRepository _userSearchRepository;

    public UserIntentionResolver(
        IUserSearchRepository userSearchRepository)
    {
        _userSearchRepository = userSearchRepository;
    }
    
    public async ValueTask<bool> IsAllowedAsync(Identity userIdentity, UserIntention intention, CancellationToken ct)
    {
        if (userIdentity.IsAdmin)
            return true;

        var user = await _userSearchRepository.GetByLoginAsync(userIdentity.Login, ct);
        if (user is null)
            throw new NotFoundUserException(userIdentity.Login);
        
        return intention switch
        {
            UserIntention.ChangeUserInfo => user.RevokedOn is null,
            _ => false
        };
    }
}