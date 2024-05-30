using AutoMapper;
using MediatR;
using Users.Application.Models;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Application.Queries.GetUserForAdmin;

public class GetUserForAdminHandler
    : IRequestHandler<GetUserForAdmin, UserModelForAdmin>
{
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IIntentionManager _intentionManager;
    private readonly IMapper _mapper;

    public GetUserForAdminHandler(
        IUserSearchRepository userSearchRepository,
        IIntentionManager intentionManager,
        IMapper mapper)
    {
        _userSearchRepository = userSearchRepository;
        _intentionManager = intentionManager;
        _mapper = mapper;
    }

    public async Task<UserModelForAdmin> Handle(GetUserForAdmin request, CancellationToken ct)
    {
        var isAllowedToGetUser = await _intentionManager
            .ResolveAsync(AdminIntention.GetUserForAdmin, ct);

        if (!isAllowedToGetUser)
            throw new IntentionException();

        var user = await _userSearchRepository.GetActiveUserByLoginAsync(request.UserLogin, ct);
        
        return user is null 
            ? throw new NotFoundUserException(request.UserLogin)
            : _mapper.Map<UserModelForAdmin>(user);
    }
}