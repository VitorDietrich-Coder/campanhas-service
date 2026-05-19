using Campanhas.Service.Microservice.Domain.Enums;

namespace Campanhas.Service.Microservice.Application.Campaigns.Models.Request;

public record CreateCampaignRequest
{
    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public decimal FinancialGoal { get; init; }

    public CampanhaStatus Status { get; init; }
}