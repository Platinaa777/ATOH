using AutoMapper;
using Users.Application.Commands.ChangeLogin;
using Users.Application.Commands.ChangePassword;
using Users.Application.Commands.ChangeUserInfo.Requests;
using Users.Application.Queries.GetUserInfo;
using Users.Domain.Users;
using Users.HttpModels.Requests;
using Users.HttpModels.Responses;

namespace Users.Api.Mappings;

public class UserRequestMapping : Profile
{
    public UserRequestMapping()
    {
        CreateMap<ChangeNameRequest, ChangeName>();
        CreateMap<ChangeBirthdayRequest, ChangeBirthday>();
        CreateMap<ChangeGenderRequest, ChangeGender>();
        CreateMap<ChangePasswordRequest, ChangePassword>();
        CreateMap<ChangeLoginRequest, ChangeLogin>();

        CreateMap<bool, ChangeUserAttributeHttpResponse>()
            .ForMember(d => d.IsSuccess, f => f.MapFrom(s => s));
        CreateMap<User, ActiveUserHttpResponse>();
        CreateMap<GetUserResponse, UserInfoHttpResponse>();
    }
}