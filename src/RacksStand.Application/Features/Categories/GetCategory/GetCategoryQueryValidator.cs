namespace RacksStand.Application.Features.Categories.GetCategory;
public class GetCategoryQueryValidator : AbstractValidator<GetCategoryQuery>
{
    public GetCategoryQueryValidator()
    {
        RuleFor(x => x.Id)
        .NotNull()
        .NotEmpty();
    }
}
