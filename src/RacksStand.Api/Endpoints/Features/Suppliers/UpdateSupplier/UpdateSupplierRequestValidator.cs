namespace RacksStand.Api.Endpoints.Features.Categories.UpdateSupplier;

public class UpdateSupplierRequestValidator : AbstractValidator<UpdateSupplierRequest>
{
    public UpdateSupplierRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(x => x.Code)
            .MaximumLength(50);
    }
}

