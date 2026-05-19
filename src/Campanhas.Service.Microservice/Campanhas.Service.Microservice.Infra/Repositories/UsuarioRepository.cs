using Campanhas.Microservice.Infrastructure.Persistence;
using Campanhas.Service.Microservice.Domain.Entities;
using Campanhas.Service.Microservice.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Campanhas.Service.Microservice.Infra.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly CampaignDbContext _context;

    public UsuarioRepository(CampaignDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Usuario usuario,
        CancellationToken cancellationToken)
    {
        await _context.Usuarios.AddAsync(usuario, cancellationToken);
    }

    public async Task<Usuario?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<Usuario?> GetByCpfAsync(
        string cpf,
        CancellationToken cancellationToken)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(x => x.CPF == cpf, cancellationToken);
    }
}