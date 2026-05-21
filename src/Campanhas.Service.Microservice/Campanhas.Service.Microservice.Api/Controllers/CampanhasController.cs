using Campanhas.Service.Microservice.Application.Campaigns.Commands.AtualizarCampanha;
using Campanhas.Service.Microservice.Application.Campaigns.Commands.CreateCampaign;
using Campanhas.Service.Microservice.Application.Campaigns.DTOs;
using Campanhas.Service.Microservice.Application.Campaigns.Models.Request;
using Campanhas.Service.Microservice.Application.Campaigns.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Payments.Microservice.API.Controllers;
using Payments.Microservice.API.Swagger.Attributes;
using Swashbuckle.AspNetCore.Annotations;

namespace Campanhas.Microservice.API.Controllers;

/// <summary>
/// Manages campaigns-related operations.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[ApiExplorerSettings(GroupName = "v1")]
[Route("api/v{version:apiVersion}/[controller]")]
public sealed class CampaignsController : ApiControllerBase
{
    public CampaignsController(IMediator mediator)
        : base()
    {
    }

    /// <summary>
    /// Creates a new campaign.
    /// </summary>
    [Authorize(Roles = "GestorONG")]
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create campaign",
        Description = "Creates a new campaign. Only users with GestorONG role can access this endpoint."
    )]
    [SwaggerResponseProfile("Campaigns.Create")]
    public async Task<ActionResult<Guid>> CreateAsync(
        [FromBody] CriarCampanhaCommand command)
    {
        var id = await Mediator.Send(command);

        return CreatedAtAction(
            nameof(GetByIdAsync),
            new { id, version = "1.0" },
            id);
    }

    /// <summary>
    /// Updates an existing campaign.
    /// </summary>
    [Authorize(Roles = "GestorONG")]
    [HttpPut("{id:guid}")]
    [SwaggerOperation(
        Summary = "Update campaign",
        Description = "Updates campaign data. Only users with GestorONG role can access this endpoint."
    )]
    [SwaggerResponseProfile("Campaigns.Update")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] Guid id,
        [FromBody] AtualizarCampanhaRequest request)
    {
        await Mediator.Send(new AtualizarCampanhaCommand(
            id,
            request.Title,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.FinancialGoal,
            request.Status));

        return NoContent();
    }

    /// <summary>
    /// Gets a campaign by its ID.
    /// </summary>
    [Authorize(Roles = "GestorONG")]
    [HttpGet("{id:guid}")]
    [SwaggerOperation(
        Summary = "Get campaign by ID",
        Description = "Returns campaign details for the given ID."
    )]
    [SwaggerResponseProfile("Campaigns.Get")]
    public async Task<ActionResult<CampaignDto>> GetByIdAsync(
        [FromRoute] Guid id)
    {
        var campaign = await Mediator.Send(
            new GetCampaignByIdQuery(id));

        return Ok(campaign);
    }

    /// <summary>
    /// Gets all active campaigns for the public transparency panel.
    /// </summary>
    [AllowAnonymous]
    [HttpGet("public")]
    [SwaggerOperation(
        Summary = "Get active campaigns",
        Description = "Returns only active campaigns with title, financial goal and total raised amount."
    )]
    [SwaggerResponseProfile("Campaigns.Public")]
    public async Task<ActionResult<IEnumerable<PublicCampaignDto>>> GetPublicAsync()
    {
        var campaigns = await Mediator.Send(
            new GetPublicActiveCampaignsQuery());

        return Ok(campaigns);
    }
}