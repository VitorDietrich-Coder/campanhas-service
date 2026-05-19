using Campanhas.Service.Microservice.Domain.Entities;

namespace Campanhas.Service.Microservice.Domain.Interfaces;

public interface IJwtService
{
    string GenerateToken(Usuario user);
}