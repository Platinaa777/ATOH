using MediatR;
using Users.Application.Security;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Shared;
using Users.Domain.Users;
using Users.Domain.Users.Repos;

namespace Users.Application.Commands.RecoverUser;

public class RecoverUserHandler
    : IRequestHandler<RecoverUser, bool>
{
    private readonly IIntentionManager _intentionManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IIdentityProvider _identityProvider;

    public RecoverUserHandler(
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
    
    public async Task<bool> Handle(RecoverUser request, CancellationToken ct)
    {
        var isAllowedToRecoverUser = await _intentionManager
            .ResolveAsync(AdminIntention.RecoverUser, ct);

        if (!isAllowedToRecoverUser)
            throw new IntentionException();
        
        var existingUser = await _userSearchRepository.GetRevokedUserByLoginAsync(request.Login, ct);
        if (existingUser is null)
            throw new NotFoundUserException(request.Login);
        
        RecoverUser(existingUser);

        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }

    private void RecoverUser(User user)
    {
        user.RevokedBy = null;
        user.RevokedOn = null;
        user.ModifiedBy = _identityProvider.CurrentIdentity.Login;
        user.ModifiedOn = DateTime.UtcNow;
    }
}