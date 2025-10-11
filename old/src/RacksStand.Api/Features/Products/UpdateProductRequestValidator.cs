namespace RacksStand.Api.Features.Products;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(x => x.Name)
          .NotEmpty().WithMessage("Product name is required.");

    }
}
