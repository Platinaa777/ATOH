using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands.DeleteUser;
using Users.Application.Commands.LoginUser;
using Users.Application.Commands.RecoverUser;
using Users.Application.Commands.RegisterUser;
using Users.Application.Queries.GetUserForAdmin;
using Users.HttpModels.Requests;
using Users.HttpModels.Responses;

namespace Users.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public AuthController(
        IMediator mediator,
        IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginHttpResponse>> Login(
        [FromBody] LoginUserRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<LoginUser>(request));

        return Ok(_mapper.Map<LoginHttpResponse>(res));
    }

    [HttpPost("register")]
    public async Task<ActionResult<RegisterHttpResponse>> Register(
        [FromBody] RegisterRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<RegisterUser>(request));

        return Ok(_mapper.Map<RegisterHttpResponse>(res));
    }

    [HttpDelete("account/delete")]
    [Authorize(Policy = "OnlyForAdmin")]
    public async Task<ActionResult<DeleteUserHttpResponse>> DeleteAccount(
        [FromBody] DeleteUserByAdminRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<DeleteUser>(request));

        return Ok(_mapper.Map<DeleteUserHttpResponse>(res));
    }
    
    [HttpPut("account/revoke")]
    [Authorize(Policy = "OnlyForAdmin")]
    public async Task<ActionResult<RevokeUserHttpResponse>> RevokeAccount(
        [FromBody] RevokeUserRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<RecoverUser>(request));

        return Ok(_mapper.Map<RevokeUserHttpResponse>(res));
    }
    
    [HttpGet("user/{login}")]
    [Authorize(Policy = "OnlyForAdmin")]
    public async Task<ActionResult<UserViewForAdmin>> GetUserInformationForAdminAccount(
        [FromRoute] string login)
    {
        var res = await _mediator.Send(new GetUserForAdmin { UserLogin = login });
        return Ok(_mapper.Map<UserViewForAdmin>(res));
    }
}