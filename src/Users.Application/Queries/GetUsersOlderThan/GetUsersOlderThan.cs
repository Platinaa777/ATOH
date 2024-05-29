using MediatR;
using Users.Application.Models;

namespace Users.Application.Queries.GetUsersOlderThan;

public class GetUsersOlderThan : IRequest<List<UserModelForAdmin>>
{
    public int Age { get; set; }    
}