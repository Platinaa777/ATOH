using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands.ChangeLogin;
using Users.Application.Commands.ChangePassword;
using Users.Application.Commands.ChangeUserInfo.Requests;
using Users.Application.Queries.GetActiveUsers;
using Users.Application.Queries.GetUserInfo;
using Users.Application.Queries.GetUsersOlderThan;
using Users.HttpModels.Requests;
using Users.HttpModels.Responses;

namespace Users.Api.Controllers;

[ApiController]
[Authorize]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public UserController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPut("name")]
    public async Task<ActionResult<ChangeUserAttributeHttpResponse>> ChangeUserName(
        [FromBody] ChangeNameRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<ChangeName>(request));
        return Ok(_mapper.Map<ChangeUserAttributeHttpResponse>(res));
    }
    
    [HttpPut("gender")]
    public async Task<ActionResult<ChangeUserAttributeHttpResponse>> ChangeUserGender(
        [FromBody] ChangeGenderRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<ChangeGender>(request));

        return Ok(_mapper.Map<ChangeUserAttributeHttpResponse>(res));
    }
    
    [HttpPut("birthday")]
    public async Task<ActionResult<ChangeUserAttributeHttpResponse>> ChangeUserName(
        [FromBody] ChangeBirthdayRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<ChangeBirthday>(request));

        return Ok(_mapper.Map<ChangeUserAttributeHttpResponse>(res));
    }
  
    [HttpPut("login")]
    public async Task<ActionResult<ChangeUserAttributeHttpResponse>> ChangeUserLogin(
        [FromBody] ChangeLoginRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<ChangeLogin>(request));

        return Ok(_mapper.Map<ChangeUserAttributeHttpResponse>(res));
    }
    
    [HttpPut("password")]
    public async Task<ActionResult<ChangeUserAttributeHttpResponse>> ChangeUserPassword(
        [FromBody] ChangePasswordRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<ChangePassword>(request));

        return Ok(_mapper.Map<ChangeUserAttributeHttpResponse>(res));
    }
    
    [HttpGet("/users")]
    [Authorize(Policy = "OnlyForAdmin")]
    public async Task<ActionResult<List<ActiveUserHttpResponse>>> GetActiveUsers()
    {
        var res = await _mediator.Send(new GetActiveUsers());
        return Ok(_mapper.Map<List<ActiveUserHttpResponse>>(res));
    }
    
    [HttpGet("info")]
    public async Task<ActionResult<UserInfoHttpResponse>> GetUserInfo()
    {
        // login fetched in middlewares from jwt token
        var res = await _mediator.Send(new GetUserInfo());
        return Ok(_mapper.Map<UserInfoHttpResponse>(res));
    }
    
    [HttpGet("/users/{age:int}")]
    [Authorize(Policy = "OnlyForAdmin")]
    public async Task<ActionResult<List<UserViewForAdmin>>> GetActiveUsersOlderThan(int age)
    {
        var res = await _mediator.Send(new GetUsersOlderThan { Age = age });
        return Ok(_mapper.Map<List<UserViewForAdmin>>(res));
    }
}