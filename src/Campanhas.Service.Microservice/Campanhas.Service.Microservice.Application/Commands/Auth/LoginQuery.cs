using Campanhas.Service.Microservice.Application.Auth.Models.Response;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Commands.Auth;

public record LoginQuery(
    string Email,
    string Password
) : IRequest<AuthResponseDto>;