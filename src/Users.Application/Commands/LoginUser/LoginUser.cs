using MediatR;

namespace Users.Application.Commands.LoginUser;

public class LoginUser : IRequest<LoginResponse>
{
    public string Login { get; set; }
    public string Password { get; set; }
}