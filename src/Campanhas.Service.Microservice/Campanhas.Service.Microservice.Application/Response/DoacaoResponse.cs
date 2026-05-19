using Campanhas.Service.Microservice.Domain.Entities;

namespace Campanhas.Service.Microservice.Application.Donations.Models.Response;

public record DoacaoResponse
{
    public Guid Id { get; set; }

    public Guid CampaignId { get; set; }

    public Guid UserId { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }

    public static explicit operator DoacaoResponse(Doacao doacao)
    {
        return new DoacaoResponse
        {
            Id = doacao.Id,
            CampaignId = doacao.CampaignId,
            UserId = doacao.UserId,
            Amount = doacao.Amount,
            CreatedAt = doacao.CreatedAt
        };
    }
}