using MediatR;

namespace Users.Application.Commands.LoginUser;

public class LoginUser : IRequest<LoginResponse>
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}