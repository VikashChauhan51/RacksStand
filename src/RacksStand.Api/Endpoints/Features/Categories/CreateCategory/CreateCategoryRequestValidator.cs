using FluentValidation;

namespace RacksStand.Api.Endpoints.Features.Categories.CreateCategory;

public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
{
    public CreateCategoryRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(255);
    }
}
