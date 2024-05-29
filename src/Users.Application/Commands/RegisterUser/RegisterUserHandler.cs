using AutoMapper;
using MediatR;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Application.Commands.RegisterUser;

public class RegisterUserHandler
    : IRequestHandler<RegisterUser, bool>
{
    private readonly IIntentionManager _intentionManager;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly IIdentityProvider _identityProvider;

    public RegisterUserHandler(
        IIntentionManager intentionManager,
        IMapper mapper,
        IUserRepository userRepository,
        IIdentityProvider identityProvider)
    {
        _intentionManager = intentionManager;
        _mapper = mapper;
        _userRepository = userRepository;
        _identityProvider = identityProvider;
    }
    
    public async Task<bool> Handle(RegisterUser request, CancellationToken ct)
    {
        var isAllowedToCreateAdmin = await _intentionManager
            .ResolveAsync(AdminIntention.CreateAdmin, ct);

        if (!isAllowedToCreateAdmin)
            request.ShouldBeAdmin = false;

        var user = _mapper.Map<User>(request);
        user.RegisterUser(GetIdentityLogin(request.Login), DateTime.Now); 
        
        await _userRepository.AddUser(user, ct);

        return true;
    }

    private string GetIdentityLogin(string currentLogin)
    {
        return string.IsNullOrWhiteSpace(_identityProvider.CurrentIdentity.Login)
            ? currentLogin
            : _identityProvider.CurrentIdentity.Login;
    }
}