using Campanhas.Service.Microservice.Application.Donations.Events;
using Campanhas.Service.Microservice.Domain.Entities;
using Campanhas.Service.Microservice.Domain.Enums;
using Campanhas.Service.Microservice.Domain.Interfaces;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Donations.Commands.CreateDonation;

public class CreateDonationCommandHandler
    : IRequestHandler<CreateDonationCommand, Unit>
{
    private readonly ICampanhaRepository _campaignRepository;

    private readonly IDoacaoRepository _donationRepository;

    private readonly IRabbitMqPublisher _publisher;

    private readonly IUnitOfWork _unitOfWork;

    public CreateDonationCommandHandler(
        ICampanhaRepository campaignRepository,
        IDoacaoRepository donationRepository,
        IRabbitMqPublisher publisher,
        IUnitOfWork unitOfWork)
    {
        _campaignRepository = campaignRepository;
        _donationRepository = donationRepository;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        CreateDonationCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetByIdAsync(
            request.CampaignId,
            cancellationToken);

        if (campaign is null)
            throw new Exception("Campaign not found.");

        if (campaign.Status != CampanhaStatus.Active)
            throw new Exception("Campaign is not active.");

        var donation = new Doacao(
            request.CampaignId,
            request.UserId,
            request.Amount);

        await _donationRepository.AddAsync(
            donation,
            cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        await _publisher.PublishAsync(
            new DonationReceivedEvent
            {
                DonationId = donation.Id,
                CampaignId = request.CampaignId,
                UserId = request.UserId,
                Amount = request.Amount,
                CreatedAt = DateTime.UtcNow
            });

        return Unit.Value;
    }
}