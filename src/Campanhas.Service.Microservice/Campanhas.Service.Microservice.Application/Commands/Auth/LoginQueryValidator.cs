using Campanhas.Service.Microservice.Application.Commands.Auth;
using FluentValidation;

namespace Campanhas.Service.Microservice.Application.Auth.Queries.Login;

public class LoginQueryValidator
    : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotEmpty();
    }
}