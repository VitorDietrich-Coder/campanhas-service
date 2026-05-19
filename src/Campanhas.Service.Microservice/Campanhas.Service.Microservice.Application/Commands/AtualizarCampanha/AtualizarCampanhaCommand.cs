using Campanhas.Service.Microservice.Domain.Enums;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Campaigns.Commands.AtualizarCampanha;

public record AtualizarCampanhaCommand(
    Guid Id,
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate,
    decimal FinancialGoal,
    CampanhaStatus Status
) : IRequest<Unit>;