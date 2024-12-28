using RacksStand.Api.Features.Shops.Endpoints.Requests;

namespace RacksStand.Api.Features.Shops.Endpoints.Validators;

public class GetShopsRequestValidator : AbstractValidator<GetShopsRequest>
{
    public GetShopsRequestValidator()
    {
        RuleFor(x => x.pageSize)
        .GreaterThan(0).WithMessage("Page size must be greater than 0.")
        .LessThanOrEqualTo(100).WithMessage("Page size cannot exceed 100.");

        RuleFor(x => x.pageNumber)
            .GreaterThan(0).WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.filter)
            .MaximumLength(100).WithMessage("Filter cannot exceed 100 characters.");
    }
}
