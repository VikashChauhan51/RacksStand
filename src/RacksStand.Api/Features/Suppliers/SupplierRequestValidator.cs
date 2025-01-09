using RacksStand.Api.Core.Validators;

namespace RacksStand.Api.Features.Suppliers;

public class SupplierRequestValidator : AbstractValidator<SupplierRequest>
{
    public SupplierRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(5, 200).WithMessage("Name should be between 5 and 200 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(3).WithMessage("Code should be 3 characters.");

        RuleFor(x => x.PrefererCommunication)
            .MaximumLength(100).WithMessage("Preferred communication should be less than 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description should be less than 500 characters.");

        RuleFor(x => x.RepresentiveName)
            .MaximumLength(100).WithMessage("Representative name should be less than 100 characters.");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Address is required.")
            .SetValidator(new AddressRequestValidator());

        RuleFor(x => x.BillingAddress)
            .NotNull().WithMessage("Billing address is required.")
            .SetValidator(new AddressRequestValidator());
    }
}
