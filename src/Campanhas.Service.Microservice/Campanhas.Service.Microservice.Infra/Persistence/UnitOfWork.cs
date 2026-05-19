using Campanhas.Microservice.Infrastructure.Persistence;
using Campanhas.Service.Microservice.Domain.Interfaces;

namespace Campanhas.Service.Microservice.Infra.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly CampaignDbContext _context;

    public UnitOfWork(CampaignDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}