using MediatR;

namespace Users.Application.Commands.RecoverUser;

public class RecoverUser : IRequest<bool>
{
    public string Login { get; set; }
}