using Campanhas.Service.Microservice.Domain.Entities;
using Campanhas.Service.Microservice.Domain.Enums;

namespace Campanhas.Service.Microservice.Application.Campaigns.Models.Response;

public sealed record CampanhaResponse
{
    public Guid Id { get; init; }

    public string Title { get; init; } = string.Empty;

    public string Description { get; init; } = string.Empty;

    public DateTime StartDate { get; init; }

    public DateTime EndDate { get; init; }

    public decimal FinancialGoal { get; init; }

    public decimal TotalRaised { get; init; }

    public CampanhaStatus Status { get; init; }

    public static explicit operator CampanhaResponse(Campanha campanha)
    {
        return new CampanhaResponse
        {
            Id = campanha.Id,
            Title = campanha.Title,
            Description = campanha.Description,
            StartDate = campanha.StartDate,
            EndDate = campanha.EndDate,
            FinancialGoal = campanha.FinancialGoal,
            TotalRaised = campanha.TotalRaised,
            Status = campanha.Status
        };
    }
}