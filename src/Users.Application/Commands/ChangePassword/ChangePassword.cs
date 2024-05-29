using MediatR;

namespace Users.Application.Commands.ChangePassword;

public class ChangePassword : IRequest<bool>
{
    public string CurrentLogin { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}