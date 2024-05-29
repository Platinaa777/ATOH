using MediatR;
using Users.Application.Authorization;
using Users.Application.Authorization.Utils;
using Users.Application.Commands.ChangeUserInfo.Requests;
using Users.Domain.Authentication;
using Users.Domain.Authorization;
using Users.Domain.Authorization.Intentions;
using Users.Domain.Exceptions;
using Users.Domain.Shared;
using Users.Domain.Users.Repos;

namespace Users.Application.Commands.ChangeUserInfo;

public class ChangeUserInfoHandler 
    :   IRequestHandler<ChangeBirthday, bool>, 
        IRequestHandler<ChangeName, bool>,
        IRequestHandler<ChangeGender, bool>
{
    private readonly IIntentionManager _intentionManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IIdentityProvider _identityProvider;

    public ChangeUserInfoHandler(
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
    
    public async Task<bool> Handle(ChangeBirthday request, CancellationToken ct)
    {
        await IdentityValidator.ValidateIsAllowedToChangeOrThrowAsync(
            _identityProvider,
            _intentionManager,
            request.Login, ct);

        var existingUser = await _userSearchRepository.GetByLoginAsync(request.Login, ct);
        if (existingUser is null)
            throw new NotFoundUserException(request.Login);

        existingUser.ChangeBirthday(request.Birthday, _identityProvider.CurrentIdentity.Login, DateTime.Now);

        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(ChangeName request, CancellationToken ct)
    {
        await IdentityValidator.ValidateIsAllowedToChangeOrThrowAsync(
            _identityProvider,
            _intentionManager,
            request.Login, ct);

        var existingUser = await _userSearchRepository.GetByLoginAsync(request.Login, ct);
        if (existingUser is null)
            throw new NotFoundUserException(request.Login);

        existingUser.ChangeName(request.Name, _identityProvider.CurrentIdentity.Login, DateTime.Now);

        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }

    public async Task<bool> Handle(ChangeGender request, CancellationToken ct)
    {
        await IdentityValidator.ValidateIsAllowedToChangeOrThrowAsync(
            _identityProvider,
            _intentionManager,
            request.Login, ct);

        var existingUser = await _userSearchRepository.GetByLoginAsync(request.Login, ct);
        if (existingUser is null)
            throw new NotFoundUserException(request.Login);

        existingUser.ChangeGender(request.Gender, _identityProvider.CurrentIdentity.Login, DateTime.Now);

        await _unitOfWork.SaveChangesAsync(ct);
        return true;
    }
}