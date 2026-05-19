using Campanhas.Service.Microservice.Domain.Entities;

namespace Campanhas.Service.Microservice.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task AddAsync(Usuario usuario, CancellationToken cancellationToken);

    Task<Usuario?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<Usuario?> GetByCpfAsync(string cpf, CancellationToken cancellationToken);
}