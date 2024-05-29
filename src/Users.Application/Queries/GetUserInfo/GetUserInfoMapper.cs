using AutoMapper;
using Users.Domain.Users;

namespace Users.Application.Queries.GetUserInfo;

public class GetUserInfoMapper : Profile
{
    public GetUserInfoMapper()
    {
        CreateMap<User, GetUserResponse>();
    }
}