using MediatR;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Application.Queries.GetActiveUsers;

public class GetActiveUserHandler
    : IRequestHandler<GetActiveUsers, List<User>>
{
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IIntentionManager _intentionManager;

    public GetActiveUserHandler(
        IUserSearchRepository userSearchRepository,
        IIntentionManager intentionManager)
    {
        _userSearchRepository = userSearchRepository;
        _intentionManager = intentionManager;
    }
    
    public async Task<List<User>> Handle(GetActiveUsers request, CancellationToken ct)
    {
        var isAllowedToGetActiveUsers = await _intentionManager
            .ResolveAsync(AdminIntention.GetActiveUsers, ct);

        if (!isAllowedToGetActiveUsers)
            throw new IntentionException();
        
        var users = await _userSearchRepository.GetActiveUsersAsync(ct);
        return users;
    }
}