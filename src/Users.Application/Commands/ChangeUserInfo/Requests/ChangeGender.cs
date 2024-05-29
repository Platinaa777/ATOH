using MediatR;

namespace Users.Application.Commands.ChangeUserInfo.Requests;

public class ChangeGender : IRequest<bool>
{
    public string Login { get; set; } = string.Empty;
    public int Gender { get; set; }
}