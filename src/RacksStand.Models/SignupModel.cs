namespace RacksStand.Models;
public record SignupModel
(
    string FullName,
    string Email,
    string? PhoneNumber,
    string Password,
    string PlanId
);
