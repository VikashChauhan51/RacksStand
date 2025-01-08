namespace RacksStand.Api.Features.Profile;

public record ProfileResponse
(
    string Id,
    string FullName,
    string Email,
    string? Phone,
    string? Theme,
    string? Local,
    bool IsEmailConfirmed,
    bool IsPhoneNumberConfirmed,
    string CompanyId
);
