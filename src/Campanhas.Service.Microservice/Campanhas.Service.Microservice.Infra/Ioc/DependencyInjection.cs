using Campanhas.Microservice.Infrastructure.Persistence;
using Campanhas.Microservice.Infrastructure.Repositories;
using Campanhas.Service.Microservice.Domain.Interfaces;
using Campanhas.Service.Microservice.Infra.Messaging;
using Campanhas.Service.Microservice.Infra.Persistence;
using Campanhas.Service.Microservice.Infra.Repositories;
using Campanhas.Service.Microservice.Infra.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Campanhas.Service.Microservice.Infra.IoC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
      this IServiceCollection services,
      IConfiguration configuration)
    {
        services.AddDbContext<CampaignDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICampanhaRepository, CampaignRepository>();
        services.AddScoped<IDoacaoRepository, DoacaoRepository>();
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IJwtService, JwtService>();

        services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();

        return services;
    }
}