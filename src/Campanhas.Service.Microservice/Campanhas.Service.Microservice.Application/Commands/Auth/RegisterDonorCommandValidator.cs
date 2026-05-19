using Campanhas.Service.Microservice.Application.Commands.Auth;
using FluentValidation;

namespace Campanhas.Service.Microservice.Application.Auth.Commands.RegisterDonor;

public class RegisterDonorCommandValidator
    : AbstractValidator<RegisterDonorCommand>
{
    public RegisterDonorCommandValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty()
            .WithMessage("Full name is required.")
            .MaximumLength(200);

        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Invalid email.");

        RuleFor(x => x.CPF)
            .NotEmpty()
            .Matches(@"^\d{11}$")
            .WithMessage("CPF must contain 11 digits.");

        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password must contain at least 6 characters.");
    }
}