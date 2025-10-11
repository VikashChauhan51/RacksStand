using RacksStand.Api.Core.Validators;
using RacksStand.Domain.Enums;

namespace RacksStand.Api.Features.Warehouses;

public class WarehouseRequestValidator : AbstractValidator<WarehouseRequest>
{
    public WarehouseRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .Length(5, 200).WithMessage("Name should be between 5 and 200 characters.");

        RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Phone number is required.")
           .MaximumLength(15).WithMessage("Phone number should be less than 15 characters.");

        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .Length(3).WithMessage("Code should be 3 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .Length(3, 100).WithMessage("Description should be between 3 and 100 characters.");

        RuleFor(x => x.DimensionUnit)
            .NotEmpty().WithMessage("DimensionUnit is required.")
            .Length(3, 100).WithMessage("DimensionUnit should be between 3 and 100 characters.")
            .Must(value => Enum.TryParse<DimensionUnit>(value, true, out var _));

        RuleFor(x => x.Length).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Width).GreaterThanOrEqualTo(0);
        RuleFor(x => x.Height).GreaterThanOrEqualTo(0);

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Address is required.");

        RuleFor(x => x.Address).SetValidator(new AddressRequestValidator());

        RuleFor(x => x.ReturnAddress)
           .NotNull().WithMessage("Return address is required.");

        RuleFor(x => x.ReturnAddress).SetValidator(new AddressRequestValidator());
    }
}
