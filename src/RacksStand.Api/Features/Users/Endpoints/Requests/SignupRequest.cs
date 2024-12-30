namespace RacksStand.Api.Features.Users.Endpoints.Requests;

public record SignupRequest
(
    string FullName,
    string Email,
    string Password,
    string PhoneNumber,
    string PlanId
);
