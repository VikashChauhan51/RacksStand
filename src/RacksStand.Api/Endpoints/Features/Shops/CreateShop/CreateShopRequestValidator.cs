namespace RacksStand.Api.Endpoints.Features.Shops.CreateShop;

public class CreateShopRequestValidator : AbstractValidator<CreateShopRequest>
{
    public CreateShopRequestValidator()
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
