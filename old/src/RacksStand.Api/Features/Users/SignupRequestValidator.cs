using RacksStand.Api.Core.Validators;

namespace RacksStand.Api.Features.Users;

public class SignupRequestValidator : AbstractValidator<SignupRequest>
{
    public SignupRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full Name is required.")
            .Length(5, 200).WithMessage("Full Name should be between 5 and 200 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.")
            .MaximumLength(150).WithMessage("Email should be less than 150 characters.");

        RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Phone number is required.")
           .MaximumLength(15).WithMessage("Phone number should be less than 15 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
            .MaximumLength(100).WithMessage("Password should be less than 100 characters.");


        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company Name is required.")
            .Length(3, 100).WithMessage("Company Name should be between 3 and 100 characters.");

        RuleFor(x => x.Address)
            .NotNull().WithMessage("Address is required.");

        RuleFor(x => x.Address).SetValidator(new AddressRequestValidator());

    }
}
