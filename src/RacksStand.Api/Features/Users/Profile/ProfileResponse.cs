namespace RacksStand.Api.Features.Users.Profile;

public record ProfileResponse
(
    string FullName,
    string Email,
    string? Phone,
    string? Theme,
    string? Local,
    bool IsEmailConfirmed,
    bool IsPhoneNumberConfirmed,
    string? CompanyName
);
