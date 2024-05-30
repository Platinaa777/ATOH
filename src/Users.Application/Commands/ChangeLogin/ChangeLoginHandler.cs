using MediatR;
using Users.Application.Authorization.Utils;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Exceptions;
using Users.Domain.Shared;
using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Application.Commands.ChangeLogin;

public class ChangeLoginHandler
    : IRequestHandler<ChangeLogin, bool>
{
    private readonly IIntentionManager _intentionManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IIdentityProvider _identityProvider;

    public ChangeLoginHandler(
        IIntentionManager intentionManager,
        IUnitOfWork unitOfWork, 
        IUserSearchRepository userSearchRepository,
        IIdentityProvider identityProvider)
    {
        _intentionManager = intentionManager;
        _unitOfWork = unitOfWork;
        _userSearchRepository = userSearchRepository;
        _identityProvider = identityProvider;
    }

    public async Task<bool> Handle(ChangeLogin request, CancellationToken ct)
    {
        await IdentityValidator.ValidateIsAllowedToChangeOrThrowAsync(
            _identityProvider,
            _intentionManager,
            request.CurrentLogin,
            ct);

        var user = await GetExistingUserAndValidateNewLoginAsync(request.CurrentLogin, request.NewLogin, ct);
        
        user.ChangeLogin(request.NewLogin, _identityProvider.CurrentIdentity.Login, DateTime.UtcNow);
        
        await _unitOfWork.SaveChangesAsync(ct);
        return true;  
    }

    private async Task<User> GetExistingUserAndValidateNewLoginAsync(string currentLogin, string newLogin, CancellationToken ct)
    {
        var existingUser = await _userSearchRepository.GetActiveUserByLoginAsync(currentLogin, ct);
        if (existingUser is null)
            throw new NotFoundUserException(currentLogin);

        var existingUserWithNewLogin = await _userSearchRepository.GetActiveUserByLoginAsync(newLogin, ct);
        if (existingUserWithNewLogin is not null)
            throw new ArgumentException(
                $"New login cant be applied, because user with login: {newLogin} is already exist");

        return existingUser;
    } 
}