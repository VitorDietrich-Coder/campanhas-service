using Campanhas.Service.Microservice.Application.Campaigns.Models.Response;
using Campanhas.Service.Microservice.Domain.Entities;
using Campanhas.Service.Microservice.Domain.Interfaces;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Campaigns.Commands.CreateCampaign;

public sealed class CriarCampanhaCommandHandler
    : IRequestHandler<CriarCampanhaCommand, CampanhaResponse>
{
    private readonly ICampanhaRepository _campanhaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CriarCampanhaCommandHandler(
        ICampanhaRepository campanhaRepository,
        IUnitOfWork unitOfWork)
    {
        _campanhaRepository = campanhaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CampanhaResponse> Handle(
        CriarCampanhaCommand request,
        CancellationToken cancellationToken)
    {
        var campanha = new Campanha(
            request.Title,
            request.Description,
            request.StartDate,
            request.EndDate,
            request.FinancialGoal);

        await _campanhaRepository.AddAsync(
            campanha,
            cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return (CampanhaResponse)campanha;
    }
}