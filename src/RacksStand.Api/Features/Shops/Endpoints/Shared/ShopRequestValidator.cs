namespace RacksStand.Api.Features.Shops.Endpoints.Shared;

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
