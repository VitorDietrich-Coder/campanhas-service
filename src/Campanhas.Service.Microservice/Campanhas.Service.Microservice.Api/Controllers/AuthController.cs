using Campanhas.Service.Microservice.Application.Auth.Models.Response;
using Campanhas.Service.Microservice.Application.Commands.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Microservice.API.Controllers;
using Payments.Microservice.API.Swagger.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Campanhas.Microservice.API.Controllers;

/// <summary>
/// Manages authentication and donor registration.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class AuthController : ApiControllerBase
{
    public AuthController(IMediator mediator) : base()
    {
    }

    /// <summary>
    /// Registers a new donor.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("register")]
    [SwaggerOperation(
        Summary = "Register donor",
        Description = "Registers a new donor with full name, unique email, CPF and hashed password."
    )]
    [SwaggerResponseProfile("Auth.Register")]
    public async Task<IActionResult> RegisterAsync(
        [FromBody] RegisterDonorCommand command)
    {
        await Mediator.Send(command);

        return Created();
    }

    /// <summary>
    /// Authenticates a user and returns a JWT token.
    /// </summary>
    [AllowAnonymous]
    [HttpPost("login")]
    [SwaggerOperation(
        Summary = "Login",
        Description = "Authenticates the user and returns a JWT token containing the user role."
    )]
    [SwaggerResponseProfile("Auth.Login")]
    public async Task<ActionResult<AuthResponseDto>> LoginAsync(
        [FromBody] LoginQuery query)
    {
        var response = await Mediator.Send(query);

        return Ok(response);
    }
}