using Campanhas.Service.Microservice.Application.Donations.Commands.CreateDonation;
using Campanhas.Service.Microservice.Application.Donations.Models.Request;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Microservice.API.Controllers;
using Payments.Microservice.API.Swagger.Attributes;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace Campanhas.Microservice.API.Controllers;

/// <summary>
/// Manages donation operations.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class DonationsController : ApiControllerBase
{
    public DonationsController(IMediator mediator)
        : base()
    {
    }

    /// <summary>
    /// Creates a donation intent for an active campaign.
    /// </summary>
    [Authorize(Roles = "Doador")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create donation",
        Description = "Creates a donation intent and publishes a DonationReceivedEvent to RabbitMQ."
    )]
    [SwaggerResponseProfile("Donations.Create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] CreateDonationRequest request)
    {
        var userId = Guid.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        await Mediator.Send(new CreateDonationCommand(
            userId,
            request.CampaignId,
            request.Amount));

        return Accepted();
    }
}