namespace RacksStand.Api.Features.Users.Endpoints.Models;

public record SignupRequest
(
    string FullName,
    string Email,
    string Password,
    string PhoneNumber,
    string PlanId
);
