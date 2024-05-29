using System.Collections.Specialized;
using MediatR;
using Users.Application.Models;
using Users.Domain.Users;

namespace Users.Application.Queries.GetUserForAdmin;

public class GetUserForAdmin : IRequest<UserModelForAdmin>
{
    public string UserLogin { get; set; } = string.Empty;
}