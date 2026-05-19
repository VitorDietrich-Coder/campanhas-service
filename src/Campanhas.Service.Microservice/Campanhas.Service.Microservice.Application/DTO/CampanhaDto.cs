using Campanhas.Service.Microservice.Domain.Enums;

namespace Campanhas.Service.Microservice.Application.Campaigns.DTOs;

public record CampaignDto
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public decimal FinancialGoal { get; init; }

    public decimal TotalRaised { get; init; }

    public CampanhaStatus Status { get; init; }

    public DateTime CreatedAt { get; init; }
}