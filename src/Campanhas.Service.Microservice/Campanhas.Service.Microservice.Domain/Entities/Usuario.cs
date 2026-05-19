using Campanhas.Service.Microservice.Domain.Core.Models;

namespace Campanhas.Service.Microservice.Domain.Entities;

public class Usuario : Entity
{
    public string FullName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string CPF { get; private set; } = string.Empty;

    public string PasswordHash { get; private set; } = string.Empty;

    public string Role { get; private set; } = "Doador";

    protected Usuario()
    {
    }

    public Usuario(
        string fullName,
        string email,
        string cpf,
        string passwordHash,
        string role = "Doador")
    {
        FullName = fullName;
        Email = email;
        CPF = cpf;
        PasswordHash = passwordHash;
        Role = role;
    }
}