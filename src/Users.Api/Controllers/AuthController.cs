using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Commands.RegisterUser;
using Users.HttpModels.Requests;

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

    [HttpGet("login")]
    public async Task<ActionResult> Login()
    {
        throw new Exception();
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest request)
    {
        var res = await _mediator.Send(_mapper.Map<RegisterUser>(request));

        if (res)
            return Ok("User successfully registered");

        return BadRequest("User was not registered");
    }

    [HttpDelete("account/delete")]
    public async Task<ActionResult> DeleteAccount([FromQuery] bool isSoftDelete)
    {
        throw new Exception();
    } 
}