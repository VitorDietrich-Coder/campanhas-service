using Campanhas.Service.Microservice.Application.Donations.Models.Response;
 using MediatR;

namespace Games.Microservice.Application.Commands.CreateGame;

public sealed record CriarCampanhaCommand(
    string Name,
    string Category,
    decimal Price
) : IRequest<DoacaoResponse>;