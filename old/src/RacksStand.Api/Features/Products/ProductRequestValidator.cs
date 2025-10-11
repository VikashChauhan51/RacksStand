namespace RacksStand.Api.Features.Products;

public class ProductRequestValidator : AbstractValidator<ProductRequest>
{
    public ProductRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Product name is required.");

        RuleFor(x => x.ProductOptions)
            .Must(options => options == null || options.Select(o => o.Name).Distinct().Count() == options.Count())
            .WithMessage("ProductOptions names must be unique.");

        RuleForEach(x => x.ProductOptions).SetValidator(new ProductOptionRequestValidator());

        RuleFor(x => x.ProductVariants)
            .NotNull().WithMessage("ProductVariants are required.")
            .NotEmpty().WithMessage("ProductVariants cannot be empty.");

        RuleForEach(x => x.ProductVariants).SetValidator(new ProductVariantRequestValidator())
            .When(x => x.ProductOptions != null)
            .WithMessage("Each ProductVariant must be valid.");

        RuleFor(x => x)
            .Must(x =>
            {
                if (x.ProductOptions == null && x.ProductVariants == null)
                    return true;

                if (x.ProductOptions == null || x.ProductVariants == null)
                    return false;

                var productOptionNames = x.ProductOptions.Select(po => po.Name).ToList();

                return x.ProductVariants.All(pv => pv.ProductVariantOptions != null &&
                    pv.ProductVariantOptions.Count() == productOptionNames.Count &&
                    pv.ProductVariantOptions.All(pvo => productOptionNames.Contains(pvo.Name)));
            })
            .WithMessage("Each ProductVariant must have ProductVariantOptions matching the ProductOptions in count and names.");
    }
}

public class ProductOptionRequestValidator : AbstractValidator<ProductOptionRequest>
{
    public ProductOptionRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("ProductOption name is required.");
    }
}

public class ProductVariantRequestValidator : AbstractValidator<ProductVariantRequest>
{
    public ProductVariantRequestValidator()
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

        RuleForEach(x => x.ProductVariantOptions)
            .SetValidator(new ProductVariantOptionRequestValidator());
    }
}

public class ProductVariantOptionRequestValidator : AbstractValidator<ProductVariantOptionRequest>
{
    public ProductVariantOptionRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("ProductVariantOption name is required.");

        RuleFor(x => x.Value)
            .NotEmpty().WithMessage("ProductVariantOption value is required.");
    }
}

