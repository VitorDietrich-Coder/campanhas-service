using Campanhas.Microservice.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Campanhas.Service.Microservice.Infra.Persistence;

public class ApplicationDbContextInitialiser
{
    private readonly CampaignDbContext _context;

    public ApplicationDbContextInitialiser(CampaignDbContext context)
    {
        _context = context;
    }

    public void Initialise()
    {
        // Early development strategy
        //_context.Database.EnsureDeleted();
        //_context.Database.EnsureCreated();
        //_context.Database.Migrate();

        // Late development strategy
        if (_context.Database.IsSqlServer())
        {
            _context.Database.Migrate();
        }
        else
        {
            _context.Database.EnsureCreated();
        }
    }
}
