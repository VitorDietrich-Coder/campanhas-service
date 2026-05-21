
using Campanhas.Service.Microservice.Application.Campaigns.Models.Response;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Campaigns.Commands.CreateCampaign;

public sealed record CriarCampanhaCommand(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    decimal FinancialGoal
) : IRequest<CampanhaResponse>;