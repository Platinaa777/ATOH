using MediatR;

namespace Users.Application.Commands.ChangeLogin;

public class ChangeLoginHandler
    : IRequestHandler<ChangeLogin, bool>
{
    public Task<bool> Handle(ChangeLogin request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}