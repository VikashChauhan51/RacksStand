namespace RacksStand.Api.Features.Users.Signup;

public record SignupRequest
(
    string FullName,
    string Email,
    string Password,
    string PhoneNumber,
    string CompanyName,
    string Address,
    string? Address2,
    string City,
    string State,
    string Country,
    string ZipCode
);
