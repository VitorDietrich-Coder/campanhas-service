using Campanhas.Service.Microservice.Application.Campaigns.Models.Request;
using Campanhas.Service.Microservice.Domain.Interfaces;
using FluentValidation;

namespace Campanhas.Service.Microservice.Application.Campaigns.Commands.CriarCampanha;

public class CriarCampanhaCommandValidator
    : AbstractValidator<CreateCampaignRequest>
{
    private readonly ICampanhaRepository _campaignRepository;

    public CriarCampanhaCommandValidator(
        ICampanhaRepository campaignRepository)
    {
        _campaignRepository = campaignRepository;

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Campaign title is required.")
            .MaximumLength(200)
            .WithMessage("Campaign title must be at most 200 characters long.");

        RuleFor(x => x.Description)
            .NotEmpty()
            .WithMessage("Campaign description is required.")
            .MaximumLength(2000)
            .WithMessage("Campaign description must be at most 2000 characters long.");

        RuleFor(x => x.StartDate)
            .NotEmpty()
            .WithMessage("Start date is required.");

        RuleFor(x => x.EndDate)
            .NotEmpty()
            .WithMessage("End date is required.")
            .GreaterThan(x => x.StartDate)
            .WithMessage("End date must be greater than start date.")
            .Must(date => date > DateTime.UtcNow)
            .WithMessage("End date cannot be in the past.");

        RuleFor(x => x.FinancialGoal)
            .GreaterThan(0)
            .WithMessage("Financial goal must be greater than zero.");
    }
}