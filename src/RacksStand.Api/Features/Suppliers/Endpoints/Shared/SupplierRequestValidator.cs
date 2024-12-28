namespace RacksStand.Api.Features.Suppliers.Endpoints.Shared;

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
