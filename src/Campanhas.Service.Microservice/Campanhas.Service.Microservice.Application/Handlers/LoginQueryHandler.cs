using BCrypt.Net;
using Campanhas.Service.Microservice.Application.Auth.Models.Response;
using Campanhas.Service.Microservice.Application.Commands.Auth;
using Campanhas.Service.Microservice.Domain.Interfaces;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Auth.Queries.Login;

public class LoginQueryHandler
    : IRequestHandler<LoginQuery, AuthResponseDto>
{
    private readonly IUsuarioRepository _userRepository;

    private readonly IJwtService _jwtService;

    public LoginQueryHandler(
        IUsuarioRepository userRepository,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto> Handle(
        LoginQuery request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository
            .GetByEmailAsync(request.Email, cancellationToken);

        if (user is null)
            throw new Exception("Invalid credentials.");

        var validPassword = BCrypt.Net.BCrypt.Verify(
            request.Password,
            user.PasswordHash);

        if (!validPassword)
            throw new Exception("Invalid credentials.");

        var token = _jwtService.GenerateToken(user);

        return new AuthResponseDto
        {
            AccessToken = token,
            FullName = user.FullName,
            Email = user.Email,
            Role = user.Role
        };
    }
}