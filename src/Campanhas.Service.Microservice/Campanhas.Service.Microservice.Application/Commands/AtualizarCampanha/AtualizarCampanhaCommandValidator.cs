using FluentValidation;

namespace Campanhas.Service.Microservice.Application.Campaigns.Commands.AtualizarCampanha;

public class AtualizarCampanhaCommandValidator
    : AbstractValidator<AtualizarCampanhaCommand>
{
    public AtualizarCampanhaCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Campaign id is required.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(2000);

        RuleFor(x => x.StartDate)
            .NotEmpty();

        RuleFor(x => x.EndDate)
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be greater than start date.");

        RuleFor(x => x.FinancialGoal)
            .GreaterThan(0)
            .WithMessage("Financial goal must be greater than zero.");
    }
}