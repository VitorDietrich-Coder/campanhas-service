using Campanhas.Service.Microservice.Domain.Entities;

namespace Campanhas.Service.Microservice.Domain.Interfaces;

public interface IDoacaoRepository
{
    Task AddAsync(Doacao doacao, CancellationToken cancellationToken);

    Task<Doacao?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Doacao>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken cancellationToken);

    Task<IEnumerable<Doacao>> GetByCampanhaIdAsync(Guid campanhaId, CancellationToken cancellationToken);
}