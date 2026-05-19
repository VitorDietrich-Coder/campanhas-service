namespace Campanhas.Service.Microservice.Application.Auth.Models.Response;

public record AuthResponseDto
{
    public string AccessToken { get; init; } = string.Empty;

    public string FullName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Role { get; init; } = string.Empty;
}