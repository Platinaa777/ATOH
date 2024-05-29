using MediatR;

namespace Users.Application.Commands.ChangeUserInfo.Requests;

public class ChangeName : IRequest<bool>
{
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
}