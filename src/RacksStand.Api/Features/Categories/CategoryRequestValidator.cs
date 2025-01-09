namespace RacksStand.Api.Features.Categories;

public class CategoryRequestValidator:AbstractValidator<CategoryRequest>    
{
    public CategoryRequestValidator()
    {
        RuleFor(x => x.Name)
       .NotEmpty().WithMessage("Name is required.")
       .Length(3, 200).WithMessage("Name should be between 3 and 200 characters.");

        RuleFor(x => x.Description)
           .NotEmpty().WithMessage("Description is required.")
           .Length(3, 100).WithMessage("Description should be between 3 and 100 characters.");
    }
}
