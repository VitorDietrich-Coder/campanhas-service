using Campanhas.Microservice.Infrastructure.Persistence;
using Campanhas.Service.Microservice.Domain.Entities;
using Campanhas.Service.Microservice.Domain.Interfaces;
using Campanhas.Service.Microservice.Infra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Campanhas.Service.Microservice.Infra.Repositories;

public class DoacaoRepository : IDoacaoRepository
{
    private readonly CampaignDbContext _context;

    public DoacaoRepository(CampaignDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Doacao doacao,
        CancellationToken cancellationToken)
    {
        await _context.Doacoes.AddAsync(doacao, cancellationToken);
    }

    public async Task<Doacao?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Doacoes
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Doacao>> GetByUsuarioIdAsync(
        Guid usuarioId,
        CancellationToken cancellationToken)
    {
        return await _context.Doacoes
            .Where(x => x.UserId == usuarioId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Doacao>> GetByCampanhaIdAsync(
        Guid campanhaId,
        CancellationToken cancellationToken)
    {
        return await _context.Doacoes
            .Where(x => x.CampaignId == campanhaId)
            .ToListAsync(cancellationToken);
    }
}