namespace Campanhas.Service.Microservice.Application.Donations.Models.Request;

public record CreateDonationRequest
{
    public Guid CampaignId { get; init; }

    public decimal Amount { get; init; }
}