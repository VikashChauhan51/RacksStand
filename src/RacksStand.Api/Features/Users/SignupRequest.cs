using RacksStand.Api.Core.Requests;

namespace RacksStand.Api.Features.Users;

public record SignupRequest
(
    string FullName,
    string Email,
    string Password,
    string PhoneNumber,
    string CompanyName,
    AddressRequest Address
);
