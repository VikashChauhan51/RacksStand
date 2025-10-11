namespace RacksStand.Api.Features.Racks;

public class CreateRackWithShelvesAndBinsRequestValidator : AbstractValidator<CreateRackWithShelvesAndBinsRequest>
{
    public CreateRackWithShelvesAndBinsRequestValidator()
    {
        RuleFor(rackRequest => rackRequest.Rack)
            .NotNull().WithMessage("Rack data is required.");

        RuleFor(rackRequest => rackRequest.Rack.Name)
            .NotEmpty().WithMessage("Rack name is required.");

        RuleFor(rackRequest => rackRequest.Rack.Code)
            .NotEmpty().WithMessage("Rack code is required.");

        RuleForEach(rackRequest => rackRequest.Rack.Shelves)
            .SetValidator(new ShelfRequestValidator());
    }
}

public class ShelfRequestValidator : AbstractValidator<ShelfRequest>
{
    public ShelfRequestValidator()
    {
        RuleFor(shelf => shelf.Name)
            .NotEmpty().WithMessage("Shelf name is required.");

        RuleFor(shelf => shelf.Code)
            .NotEmpty().WithMessage("Shelf code is required.");

        RuleForEach(shelf => shelf.Bins)
            .SetValidator(new BinRequestValidator());
    }
}

public class BinRequestValidator : AbstractValidator<BinRequest>
{
    public BinRequestValidator()
    {
        RuleFor(bin => bin.Name)
            .NotEmpty().WithMessage("Bin name is required.");

        RuleFor(bin => bin.Code)
            .NotEmpty().WithMessage("Bin code is required.");

        RuleFor(bin => bin.XAxis)
            .NotNull().WithMessage("Bin X-axis is required.");

        RuleFor(bin => bin.YAxis)
            .NotNull().WithMessage("Bin Y-axis is required.");

        RuleFor(bin => bin.Height)
            .GreaterThan(0).WithMessage("Bin height must be greater than zero.");

        RuleFor(bin => bin.Width)
            .GreaterThan(0).WithMessage("Bin width must be greater than zero.");

        RuleFor(bin => bin.Length)
            .GreaterThan(0).WithMessage("Bin length must be greater than zero.");
    }
}
