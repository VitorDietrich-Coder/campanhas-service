using Campanhas.Service.Microservice.Application.Campaigns.DTOs;
using Campanhas.Service.Microservice.Application.Campaigns.Queries;
using Campanhas.Service.Microservice.Domain.Interfaces;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Campaigns.Handlers;

public class GetPublicActiveCampaignsQueryHandler
    : IRequestHandler<GetPublicActiveCampaignsQuery, IEnumerable<PublicCampaignDto>>
{
    private readonly ICampanhaRepository _campaignRepository;

    public GetPublicActiveCampaignsQueryHandler(ICampanhaRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<IEnumerable<PublicCampaignDto>> Handle(
        GetPublicActiveCampaignsQuery request,
        CancellationToken cancellationToken)
    {
        var campaigns = await _campaignRepository.GetActiveAsync(cancellationToken);

        return campaigns.Select(campaign => new PublicCampaignDto
        {
            Id = campaign.Id,
            Title = campaign.Title,
            FinancialGoal = campaign.FinancialGoal,
            TotalRaised = campaign.TotalRaised
        });
    }
}