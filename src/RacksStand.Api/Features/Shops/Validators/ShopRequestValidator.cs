using RacksStand.Api.Features.Shops.Models;

namespace RacksStand.Api.Features.Shops.Validators;

public class ShopRequestValidator : AbstractValidator<ShopRequest>
{
    public ShopRequestValidator()
    {
        RuleFor(x => x.Name)
        .NotNull()
        .NotEmpty()
        .MaximumLength(150);

        RuleFor(x => x.Address)
            .NotNull()
            .NotEmpty()
            .MaximumLength(300);

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}
