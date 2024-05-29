using MediatR;

namespace Users.Application.Commands.ChangeLogin;

public class ChangeLogin : IRequest<bool>
{
    public string CurrentLogin { get; set; } = string.Empty;
    public string NewLogin { get; set; } = string.Empty;
}