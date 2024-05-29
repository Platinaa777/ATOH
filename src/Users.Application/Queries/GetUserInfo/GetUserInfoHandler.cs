using AutoMapper;
using MediatR;
using Users.Application.Queries.GetUserForAdmin;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Users.Repos;

namespace Users.Application.Queries.GetUserInfo;

public class GetUserInfoHandler
    : IRequestHandler<GetUserInfo, GetUserResponse>
{
    private readonly IIntentionManager _intentionManager;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IIdentityProvider _identityProvider;
    private readonly IMapper _mapper;

    public GetUserInfoHandler(
        IIntentionManager intentionManager,
        IUserSearchRepository userSearchRepository,
        IIdentityProvider identityProvider,
        IMapper mapper)
    {
        _intentionManager = intentionManager;
        _userSearchRepository = userSearchRepository;
        _identityProvider = identityProvider;
        _mapper = mapper;
    }
    
    public async Task<GetUserResponse> Handle(GetUserInfo request, CancellationToken ct)
    {
        var isAllowedToGetUser = await _intentionManager
            .ResolveAsync(UserIntention.GetUserInfo, ct);

        if (!isAllowedToGetUser)
            throw new IntentionException();

        var user = await _userSearchRepository.GetByLoginAsync(_identityProvider.CurrentIdentity.Login, ct);
        
        return user is null 
            ? throw new NotFoundUserException(_identityProvider.CurrentIdentity.Login)
            : _mapper.Map<GetUserResponse>(user);
    }
}