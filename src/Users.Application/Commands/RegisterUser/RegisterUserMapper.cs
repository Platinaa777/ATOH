using AutoMapper;
using Users.Domain.Users;

namespace Users.Application.Commands.RegisterUser;

public class RegisterUserMapper : Profile
{
    public RegisterUserMapper()
    {
        CreateMap<RegisterUser, User>()
            .ForMember(d => d.IsAdmin, s => s.MapFrom(f => f.ShouldBeAdmin));
    }
}