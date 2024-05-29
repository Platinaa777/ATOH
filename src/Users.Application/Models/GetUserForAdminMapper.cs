using AutoMapper;
using Users.Domain.Users;

namespace Users.Application.Models;

public class GetUserForAdminMapper : Profile
{
    public GetUserForAdminMapper()
    {
        CreateMap<User, UserModelForAdmin>();
    }
}