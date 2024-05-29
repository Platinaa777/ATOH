using MediatR;
using Users.Domain.Users;

namespace Users.Application.Queries.GetActiveUsers;

public class GetActiveUsers : IRequest<List<User>>
{
    
}