using FluentValidation;

namespace Campanhas.Service.Microservice.Application.Donations.Commands.CreateDonation;

public class CreateDonationCommandValidator
    : AbstractValidator<CreateDonationCommand>
{
    public CreateDonationCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User id is required.");

        RuleFor(x => x.CampaignId)
            .NotEmpty()
            .WithMessage("Campaign id is required.");

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithMessage("Donation amount must be greater than zero.");
    }
}