namespace RacksStand.Api.Features.Users.Signup;

using FluentValidation;

public class SignupRequestValidator : AbstractValidator<SignupRequest>
{
    public SignupRequestValidator()
    {
        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full Name is required.")
            .Length(5, 100).WithMessage("Full Name should be between 5 and 100 characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid Email format.");

        RuleFor(x => x.PhoneNumber)
           .NotEmpty().WithMessage("Phone number is required.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.CompanyName)
            .NotEmpty().WithMessage("Company Name is required.")
            .Length(2, 100).WithMessage("Company Name should be between 2 and 100 characters.");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address is required.");

        RuleFor(x => x.City)
            .NotEmpty().WithMessage("City is required.");

        RuleFor(x => x.State)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.Country)
            .NotEmpty().WithMessage("Country is required.");

        RuleFor(x => x.ZipCode)
            .NotEmpty().WithMessage("Zip Code is required.");
    }
}
