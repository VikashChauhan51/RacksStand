namespace RacksStand.Api.Core.Requests;

public record AddressRequest
(
    string Address,
    string? Address2,
    string City,
    string State,
    string Country,
    string ZipCode
);

