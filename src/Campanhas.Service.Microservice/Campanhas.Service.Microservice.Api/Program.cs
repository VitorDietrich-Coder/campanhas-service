using Campanhas.Microservice.Infrastructure.Persistence;
using Campanhas.Microservice.Infrastructure.Repositories;
using Campanhas.Service.Microservice.Api.Extensions;
using Campanhas.Service.Microservice.Domain.Interfaces;
using Campanhas.Service.Microservice.Infra.IoC;
using Campanhas.Service.Microservice.Infra.Persistence;
using Campanhas.Service.Microservice.Infra.Repositories;
using Campanhas.Service.Microservice.Infra.Security;
using Microsoft.EntityFrameworkCore;
using Payments.Microservice.API.Extensions;
using Prometheus;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CampaignDbContext>(options =>
    options.UseSqlServer(connectionString),
    ServiceLifetime.Scoped); 

builder.Services.AddScoped<ApplicationDbContextInitialiser>();
builder.Services.AddOpenTelemetryTracing(builder.Configuration);
builder.Services.AddApiVersioningConfiguration();
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerServices();

builder.Services.AddGlobalCorsPolicy();

builder.Services.AddHttpContextAccessor();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddOpenApi();
builder.Host.UseSerilog((context, services, configuration) =>
{
    SerilogExtensions.ConfigureSerilog(context, services, configuration);
});
builder.Services.AddCustomAuthentication(builder.Configuration);
 

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CampaignDbContext>();

    var retries = 10;

    while (retries > 0)
    {
        try
        {
            await context.Database.MigrateAsync();

            await DbInitializer.SeedAsync(context);
            break;
        }
        catch
        {
            retries--;
            await Task.Delay(5000);
        }
    }
}

app.UseSwaggerWithUI();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseMiddleware<UnauthorizedResponseMiddleware>();
app.UseAuthorization();

app.UseHttpMetrics();

app.MapControllers();

app.MapMetrics();

app.Run();
 