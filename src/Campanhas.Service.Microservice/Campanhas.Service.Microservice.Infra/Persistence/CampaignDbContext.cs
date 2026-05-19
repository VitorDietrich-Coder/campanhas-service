using Campanhas.Microservice.Infrastructure.EventStore;
using Campanhas.Service.Microservice.Domain.Entities;
using Campanhas.Service.Microservice.Infra.MapEntities;
using Microsoft.EntityFrameworkCore;
using Payments.Microservice.Domain.Core.Events;

namespace Campanhas.Microservice.Infrastructure.Persistence;

public class CampaignDbContext : DbContext
{
    public CampaignDbContext(DbContextOptions<CampaignDbContext> options)
        : base(options)
    {
    }

    public DbSet<Campanha> Campanhas { get; set; }

    public DbSet<Doacao> Doacoes { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<StoredEvent> StoredEvents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<DomainEvent>();
        modelBuilder.Ignore<List<DomainEvent>>();
        modelBuilder.Ignore<IReadOnlyCollection<DomainEvent>>();

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(CampanhaConfiguration).Assembly
        );
    }
}