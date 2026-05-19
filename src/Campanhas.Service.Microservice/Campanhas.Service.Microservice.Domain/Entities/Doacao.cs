
using Campanhas.Service.Microservice.Domain.Core.Models;

namespace Campanhas.Service.Microservice.Domain.Entities;

public class Doacao : Entity
{
    public Guid CampaignId { get; private set; }

    public Guid UserId { get; private set; }

    public decimal Amount { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public Campanha Campanha { get; private set; } = null!;

    public Usuario User { get; private set; } = null!;

    protected Doacao()
    {
    }

    public Doacao(
        Guid campaignId,
        Guid userId,
        decimal amount)
    {
        if (amount <= 0)
            throw new Exception("Donation amount must be greater than zero.");

        CampaignId = campaignId;

        UserId = userId;

        Amount = amount;

        CreatedAt = DateTime.UtcNow;
    }
}