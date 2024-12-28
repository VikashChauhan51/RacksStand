namespace RacksStand.Api.Core.Infrastructure.Validator;

public class IdValidator : AbstractValidator<string>
{
    private const int MaxLength = 100;
    public IdValidator()
    {
        RuleFor(id => id)
            .NotNull().WithMessage("ID cannot be null.")
            .NotEmpty().WithMessage("ID cannot be empty.")
            .MaximumLength(MaxLength).WithMessage($"ID cannot exceed {MaxLength} characters.");
    }
}
