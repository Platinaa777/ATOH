using MediatR;
using Users.Application.Authorization.Utils;
using Users.Application.Security;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Exceptions;
using Users.Domain.Shared;
using Users.Domain.Users.Repos;

namespace Users.Application.Commands.ChangePassword;

public class ChangePasswordHandler  
    : IRequestHandler<ChangePassword, bool>
{
    private readonly IIntentionManager _intentionManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IIdentityProvider _identityProvider;
    private readonly IHasherPassword _hasherPassword;

    public ChangePasswordHandler(
        IIntentionManager intentionManager,
        IUnitOfWork unitOfWork, 
        IUserSearchRepository userSearchRepository,
        IIdentityProvider identityProvider,
        IHasherPassword hasherPassword)
    {
        _intentionManager = intentionManager;
        _unitOfWork = unitOfWork;
        _userSearchRepository = userSearchRepository;
        _identityProvider = identityProvider;
        _hasherPassword = hasherPassword;
    }
    
    public async Task<bool> Handle(ChangePassword request, CancellationToken ct)
    {
        await IdentityValidator.ValidateIsAllowedToChangeOrThrowAsync(
            _identityProvider,
            _intentionManager,
            request.CurrentLogin,
            ct);

        var user = await _userSearchRepository.GetByLoginAsync(request.CurrentLogin, ct);
        if (user is null)
            throw new NotFoundUserException(request.CurrentLogin);

        var hashedPassword = _hasherPassword.HashPassword(request.NewPassword);
        user.ChangePassword(hashedPassword, _identityProvider.CurrentIdentity.Login, DateTime.Now);
        
        await _unitOfWork.SaveChangesAsync(ct);
        return true; 
    }
}