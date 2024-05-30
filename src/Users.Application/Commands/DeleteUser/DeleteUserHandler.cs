using MediatR;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Shared;
using Users.Domain.Users.Repos;

namespace Users.Application.Commands.DeleteUser;

public class DeleteUserHandler
    : IRequestHandler<DeleteUser, bool>
{
    private readonly IIntentionManager _intentionManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IIdentityProvider _identityProvider;
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(
        IIntentionManager intentionManager,
        IUnitOfWork unitOfWork, 
        IUserSearchRepository userSearchRepository,
        IIdentityProvider identityProvider,
        IUserRepository userRepository)
    {
        _intentionManager = intentionManager;
        _unitOfWork = unitOfWork;
        _userSearchRepository = userSearchRepository;
        _identityProvider = identityProvider;
        _userRepository = userRepository;
    }
    
    public async Task<bool> Handle(DeleteUser request, CancellationToken ct)
    {
        var isAllowedToDelete = await _intentionManager
            .ResolveAsync(AdminIntention.DeleteUser, ct);

        if (!isAllowedToDelete)
            throw new IntentionException();

        var existingUser = await _userSearchRepository.GetActiveUserByLoginAsync(request.Login, ct);
        if (existingUser is null)
            throw new NotFoundUserException(request.Login);

        if (request.IsSoftDelete)
            await _userRepository.SoftDeleteAsync(existingUser.Id, _identityProvider.CurrentIdentity.Login, ct);
        else
            await _userRepository.ForceDeleteAsync(existingUser.Id, ct);
        
        
        await _unitOfWork.SaveChangesAsync(ct);
        return true;   
    }
}