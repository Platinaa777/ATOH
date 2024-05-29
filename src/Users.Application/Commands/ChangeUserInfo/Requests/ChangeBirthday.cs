using MediatR;

namespace Users.Application.Commands.ChangeUserInfo.Requests;

public class ChangeBirthday : IRequest<bool>
{
    public string Login { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
}