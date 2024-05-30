using AutoMapper;
using MediatR;
using Users.Application.Models;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Users.Repos;

namespace Users.Application.Queries.GetUsersOlderThan;

public class GetUsersOlderThanHandler
    : IRequestHandler<GetUsersOlderThan, List<UserModelForAdmin>>
{
    private readonly IIntentionManager _intentionManager;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IMapper _mapper;

    public GetUsersOlderThanHandler(
        IIntentionManager intentionManager,
        IUserSearchRepository userSearchRepository,
        IMapper mapper)
    {
        _intentionManager = intentionManager;
        _userSearchRepository = userSearchRepository;
        _mapper = mapper;
    }
    
    public async Task<List<UserModelForAdmin>> Handle(GetUsersOlderThan request, CancellationToken ct)
    {
        var isAllowedToGetUser = await _intentionManager
            .ResolveAsync(AdminIntention.GetUserForAdmin, ct);

        if (!isAllowedToGetUser)
            throw new IntentionException();

        var users = await _userSearchRepository.GetUserWithAgeBiggerThanAsync(request.Age, ct);

        return users.Select(user => _mapper.Map<UserModelForAdmin>(user)).ToList();
    }
}