using AutoMapper;
using MediatR;
using Users.Application.Security;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Shared;
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
    private readonly IHasherPassword _hasherPassword;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserHandler(
        IIntentionManager intentionManager,
        IMapper mapper,
        IUserRepository userRepository,
        IIdentityProvider identityProvider,
        IHasherPassword hasherPassword,
        IUserSearchRepository userSearchRepository,
        IUnitOfWork unitOfWork)
    {
        _intentionManager = intentionManager;
        _mapper = mapper;
        _userRepository = userRepository;
        _identityProvider = identityProvider;
        _hasherPassword = hasherPassword;
        _userSearchRepository = userSearchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(RegisterUser request, CancellationToken ct)
    {
        var isAllowedToCreateAdmin = await _intentionManager
            .ResolveAsync(AdminIntention.CreateAdmin, ct);

        if (!isAllowedToCreateAdmin)
            request.ShouldBeAdmin = false;

        await ValidateUserIsNotExistOrThrow(request.Login, ct);

        var user = _mapper.Map<User>(request);
        user.RegisterUser(
            Guid.NewGuid(),
            createdBy: GetIdentityLogin(request.Login),
            creationTime: DateTime.UtcNow,
            _hasherPassword.HashPassword(request.Password));

        await _userRepository.AddUserAsync(user, ct);
        await _unitOfWork.SaveChangesAsync(ct);

        return true;
    }

    private string GetIdentityLogin(string currentLogin)
    {
        return string.IsNullOrWhiteSpace(_identityProvider.CurrentIdentity.Login) || Guid.Empty.ToString() == currentLogin
            ? currentLogin
            : _identityProvider.CurrentIdentity.Login;
    }

    private async Task ValidateUserIsNotExistOrThrow(string login, CancellationToken ct)
    {
        var existingUser = await _userSearchRepository.GetActiveUserByLoginAsync(login, ct);
        if (existingUser is not null)
            throw new DuplicateUserException(login);
    }
}