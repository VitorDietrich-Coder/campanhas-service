namespace Campanhas.Service.Microservice.Application.Campaigns.DTOs;

public record PublicCampaignDto
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public decimal FinancialGoal { get; init; }

    public decimal TotalRaised { get; init; }
}