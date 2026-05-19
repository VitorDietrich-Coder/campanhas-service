using MediatR;

namespace Campanhas.Service.Microservice.Application.Donations.Commands.CreateDonation;

public record CreateDonationCommand(
    Guid UserId,
    Guid CampaignId,
    decimal Amount
) : IRequest<Unit>;