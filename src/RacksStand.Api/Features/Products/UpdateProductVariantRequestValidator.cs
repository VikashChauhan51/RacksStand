namespace RacksStand.Api.Features.Products;

public class UpdateProductVariantRequestValidator:AbstractValidator<UpdateProductVariantRequest>    
{
    public UpdateProductVariantRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("ProductVariant name is required.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("ProductVariant code is required.");

        RuleFor(x => x.Sku)
            .NotEmpty().WithMessage("ProductVariant SKU is required.");

        RuleFor(x => x.PurchasePrice)
            .GreaterThan(0).WithMessage("PurchasePrice must be greater than 0.");

        RuleFor(x => x.SellingPrice)
            .GreaterThan(0).WithMessage("SellingPrice must be greater than 0.");

        RuleFor(x => x.CurrencyCode)
            .NotEmpty().WithMessage("CurrencyCode is required.");

        RuleFor(x => x.Height)
            .GreaterThanOrEqualTo(0).WithMessage("Height must be greater than or equal to 0.");

        RuleFor(x => x.Width)
            .GreaterThanOrEqualTo(0).WithMessage("Width must be greater than or equal to 0.");

        RuleFor(x => x.Length)
            .GreaterThanOrEqualTo(0).WithMessage("Length must be greater than or equal to 0.");

        RuleFor(x => x.Weight)
            .GreaterThanOrEqualTo(0).WithMessage("Weight must be greater than or equal to 0.");
    }
}
