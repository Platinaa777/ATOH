using MediatR;

namespace Users.Application.Commands.RegisterUser;

public class RegisterUser : IRequest<bool>
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public int Gender { get; set; }
    public DateTime Birthday { get; set; }
    public bool ShouldBeAdmin { get; set; }
}