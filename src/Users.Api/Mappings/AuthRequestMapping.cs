using AutoMapper;
using Users.Application.Commands.DeleteUser;
using Users.Application.Commands.LoginUser;
using Users.Application.Commands.RecoverUser;
using Users.Application.Commands.RegisterUser;
using Users.Application.Models;
using Users.HttpModels.Requests;
using Users.HttpModels.Responses;

namespace Users.Api.Mappings;

public class AuthRequestMapping : Profile
{
    public AuthRequestMapping()
    {
        CreateMap<RegisterRequest, RegisterUser>();
        CreateMap<LoginUserRequest, LoginUser>();
        CreateMap<DeleteUserByAdminRequest, DeleteUser>();
        CreateMap<RevokeUserRequest, RecoverUser>();

        CreateMap<LoginResponse, LoginHttpResponse>();
        CreateMap<bool, RegisterHttpResponse>()
            .ForMember(d => d.IsSuccess, f => f.MapFrom(s => s));
        CreateMap<bool, DeleteUserHttpResponse>()
            .ForMember(d => d.IsSuccess, f => f.MapFrom(s => s));
        CreateMap<bool, RevokeUserHttpResponse>()
            .ForMember(d => d.IsSuccess, f => f.MapFrom(s => s));
        CreateMap<UserModelForAdmin, UserViewForAdmin>();
    }
}