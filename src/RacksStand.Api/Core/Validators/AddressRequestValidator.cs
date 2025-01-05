using RacksStand.Api.Core.Requests;

namespace RacksStand.Api.Core.Validators;

public class AddressRequestValidator : AbstractValidator<AddressRequest>
{
    public AddressRequestValidator()
    {
        RuleFor(x => x.Address)
           .NotEmpty().WithMessage("Address is required.")
           .MaximumLength(255).WithMessage("Address should be less than 255 characters.");

        RuleFor(x => x.Address2)
          .MaximumLength(255).WithMessage("Address2 should be less than 255 characters.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.")
        .MaximumLength(100).WithMessage("Address2 should be less than 100 characters.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.")
            .MaximumLength(100).WithMessage("Address2 should be less than 100 characters.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.")
            .MaximumLength(100).WithMessage("Address2 should be less than 100 characters.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Zip Code is required.")
            .MaximumLength(20).WithMessage("Address2 should be less than 20 characters.");
    }
}
