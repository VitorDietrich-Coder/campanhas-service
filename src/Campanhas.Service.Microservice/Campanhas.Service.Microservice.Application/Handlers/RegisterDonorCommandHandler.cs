using BCrypt.Net;
using Campanhas.Service.Microservice.Application.Commands.Auth;
using Campanhas.Service.Microservice.Domain.Entities;
using Campanhas.Service.Microservice.Domain.Interfaces;
using MediatR;

namespace Campanhas.Service.Microservice.Application.Auth.Commands.RegisterDonor;

public class RegisterDonorCommandHandler
    : IRequestHandler<RegisterDonorCommand, Unit>
{
    private readonly IUsuarioRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterDonorCommandHandler(
        IUsuarioRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        RegisterDonorCommand request,
        CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository
            .GetByEmailAsync(request.Email, cancellationToken);

        if (existingUser is not null)
            throw new Exception("Email already registered.");

        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var user = new Usuario(
            request.FullName,
            request.Email,
            request.CPF,
            passwordHash,
            "Doador");

        await _userRepository.AddAsync(user, cancellationToken);

        await _unitOfWork.CommitAsync(cancellationToken);

        return Unit.Value;
    }
}