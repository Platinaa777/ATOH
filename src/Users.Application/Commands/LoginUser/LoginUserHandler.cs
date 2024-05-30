using MediatR;
using Users.Application.Security;
using Users.Domain.Authentication;
using Users.Domain.Exceptions;
using Users.Domain.Users.Repos;

namespace Users.Application.Commands.LoginUser;

public class LoginUserHandler
    : IRequestHandler<LoginUser, LoginResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserSearchRepository _userSearchRepository;
    private readonly IHasherPassword _hasherPassword;
    private readonly IAuthenticationService _authenticationService;

    public LoginUserHandler(
        IUserRepository userRepository,
        IUserSearchRepository userSearchRepository,
        IHasherPassword hasherPassword,
        IAuthenticationService authenticationService)
    {
        _userRepository = userRepository;
        _userSearchRepository = userSearchRepository;
        _hasherPassword = hasherPassword;
        _authenticationService = authenticationService;
    }
    
    public async Task<LoginResponse> Handle(LoginUser request, CancellationToken ct)
    {
        var existingUser = await _userSearchRepository.GetActiveUserByLoginAsync(request.Login, ct);
        
        if (existingUser is null || existingUser.RevokedOn is not null)
            throw new NotFoundUserException(request.Login);

        var isPasswordMatching = _hasherPassword.Verify(existingUser.Password, request.Password);
        if (!isPasswordMatching)
            return new LoginResponse(false, string.Empty, "Неверный пароль");

        var newToken = _authenticationService.GenerateAccessToken(existingUser);

        return new LoginResponse(true, newToken);
    }
}