using Campanhas.Service.Microservice.Application.Campaigns.DTOs;
using Campanhas.Service.Microservice.Application.Campaigns.Queries;
using Campanhas.Service.Microservice.Domain.Interfaces;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Campaigns.Handlers;

public class GetCampaignByIdQueryHandler
    : IRequestHandler<GetCampaignByIdQuery, CampaignDto>
{
    private readonly ICampanhaRepository _campaignRepository;

    public GetCampaignByIdQueryHandler(ICampanhaRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<CampaignDto> Handle(
        GetCampaignByIdQuery request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (campaign is null)
            throw new Exception("Campanha não encontrada.");

        return new CampaignDto
        {
            Id = campaign.Id,
            Title = campaign.Title,
            Description = campaign.Description,
            StartDate = campaign.StartDate,
            EndDate = campaign.EndDate,
            FinancialGoal = campaign.FinancialGoal,
            TotalRaised = campaign.TotalRaised,
            Status = campaign.Status,
        };
    }
}