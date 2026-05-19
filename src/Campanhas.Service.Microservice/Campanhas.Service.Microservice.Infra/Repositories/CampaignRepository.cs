 
using Campanhas.Microservice.Infrastructure.Persistence;
using Campanhas.Service.Microservice.Domain.Entities;
using Campanhas.Service.Microservice.Domain.Enums;
using Campanhas.Service.Microservice.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Payments.Microservice.Domain.Interfaces;

namespace Campanhas.Microservice.Infrastructure.Repositories;

public class CampaignRepository : ICampanhaRepository
{
    private readonly CampaignDbContext _context;

    public CampaignRepository(CampaignDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Campanha campanha,
        CancellationToken cancellationToken)
    {
        await _context.Campanhas.AddAsync(campanha, cancellationToken);
    }

    public async Task<Campanha?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Campanhas
            .FirstOrDefaultAsync(campaign => campaign.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Campanha>> GetActiveAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Campanhas
            .Where(campaign => campaign.Status == CampanhaStatus.Active)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(
        Campanha campaign,
        CancellationToken cancellationToken)
    {
        var existingCampaign = await _context.Campanhas
            .FirstOrDefaultAsync(x => x.Id == campaign.Id, cancellationToken);

        if (existingCampaign is null)
            return;

        _context.Entry(existingCampaign).CurrentValues.SetValues(campaign);
    }
     
}