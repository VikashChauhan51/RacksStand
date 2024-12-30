using RacksStand.Api.Features.Suppliers.Models;

namespace RacksStand.Api.Features.Suppliers.Validators;

public class SupplierRequestValidator : AbstractValidator<SupplierRequest>
{
    public SupplierRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Code)
            .MaximumLength(50);
    }
}
