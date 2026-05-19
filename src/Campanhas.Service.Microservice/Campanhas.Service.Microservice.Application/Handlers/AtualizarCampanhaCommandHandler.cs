using Campanhas.Service.Microservice.Domain.Interfaces;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Campaigns.Commands.AtualizarCampanha;

public class AtualizarCampanhaCommandHandler
    : IRequestHandler<AtualizarCampanhaCommand, Unit>
{
    private readonly ICampanhaRepository _campaignRepository;

    public AtualizarCampanhaCommandHandler(
        ICampanhaRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;
    }

    public async Task<Unit> Handle(
        AtualizarCampanhaCommand request,
        CancellationToken cancellationToken)
    {
        var campaign = await _campaignRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (campaign is null)
            throw new Exception("Campaign not found.");

        campaign.Update(
            request.Title,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.FinancialGoal,
            request.Status);

        await _campaignRepository.UpdateAsync(
            campaign,
            cancellationToken);

        return Unit.Value;
    }
}