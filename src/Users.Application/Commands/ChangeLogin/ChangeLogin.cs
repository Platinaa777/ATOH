using MediatR;

namespace Users.Application.Commands.ChangeLogin;

public class ChangeLogin : IRequest<bool>
{
    public string NewLogin { get; set; } = string.Empty;
}