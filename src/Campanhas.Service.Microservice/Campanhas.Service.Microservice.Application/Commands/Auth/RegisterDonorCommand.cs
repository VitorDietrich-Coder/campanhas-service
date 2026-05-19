using MediatR;

namespace Campanhas.Service.Microservice.Application.Commands.Auth;

public record RegisterDonorCommand(
    string FullName,
    string Email,
    string CPF,
    string Password
) : IRequest<Unit>;