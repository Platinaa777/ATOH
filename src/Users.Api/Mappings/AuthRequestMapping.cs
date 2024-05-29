using AutoMapper;
using Users.Application.Commands.RegisterUser;
using Users.HttpModels.Requests;

namespace Users.Api.Mappings;

public class AuthRequestMapping : Profile
{
    public AuthRequestMapping()
    {
        CreateMap<RegisterRequest, RegisterUser>();
    }
}