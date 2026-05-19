using Campanhas.Service.Microservice.Domain.Entities;

namespace Campanhas.Service.Microservice.Domain.Interfaces;

public interface ICampanhaRepository
{
    Task<Campanha> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken);
    Task AddAsync(Campanha campanha, CancellationToken cancellationToken);
    Task UpdateAsync(Campanha campanha, CancellationToken cancellationToken);
    Task<IEnumerable<Campanha>> GetActiveAsync(CancellationToken cancellationToken);
}
