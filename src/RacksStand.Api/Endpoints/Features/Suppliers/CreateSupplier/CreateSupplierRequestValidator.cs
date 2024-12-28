namespace RacksStand.Api.Endpoints.Features.Suppliers.CreateSupplier;

public class CreateSupplierRequestValidator : AbstractValidator<CreateSupplierRequest>
{
    public CreateSupplierRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Code)
            .MaximumLength(50);
    }
}
