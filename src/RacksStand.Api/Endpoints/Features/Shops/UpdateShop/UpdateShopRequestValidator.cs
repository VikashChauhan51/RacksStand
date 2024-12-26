namespace RacksStand.Api.Endpoints.Features.Shops.UpdateShop;

public class UpdateShopRequestValidator : AbstractValidator<UpdateShopRequest>
{
    public UpdateShopRequestValidator()
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
