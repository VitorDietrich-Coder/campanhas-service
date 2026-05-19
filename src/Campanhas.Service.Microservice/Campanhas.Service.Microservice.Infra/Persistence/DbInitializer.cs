using Campanhas.Microservice.Infrastructure.Persistence;
using Campanhas.Service.Microservice.Domain.Entities;

namespace Campanhas.Service.Microservice.Infra.Persistence;

public static class DbInitializer
{
    public static async Task SeedAsync(CampaignDbContext context)
    {
        if (context.Usuarios.Any())
            return;

        var admin = new Usuario(
            "Administrador",
            "admin@conexaosolidaria.com",
            "11111111111",
            BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            "GestorONG");

        var doador = new Usuario(
            "Doador Teste",
            "doador@conexaosolidaria.com",
            "22222222222",
            BCrypt.Net.BCrypt.HashPassword("Doador@123"),
            "Doador");

        await context.Usuarios.AddRangeAsync(admin, doador);

        await context.Campanhas.AddRangeAsync(
            new Campanha(
                "Campanha Inverno",
                "Arrecadação de roupas",
                DateTime.UtcNow,
                DateTime.UtcNow.AddMonths(1),
                10000),

            new Campanha(
                "Campanha Natal",
                "Arrecadação de brinquedos",
                DateTime.UtcNow,
                DateTime.UtcNow.AddMonths(2),
                15000)
        );

        await context.SaveChangesAsync();
    }
}