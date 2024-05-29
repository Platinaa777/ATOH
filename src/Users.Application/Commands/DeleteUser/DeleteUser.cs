using MediatR;

namespace Users.Application.Commands.DeleteUser;

public class DeleteUser : IRequest<bool>
{
    public string Login { get; set; } = string.Empty;
    public bool IsSoftDelete { get; set; }
}