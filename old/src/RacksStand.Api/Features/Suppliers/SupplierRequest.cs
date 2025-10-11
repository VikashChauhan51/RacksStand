using RacksStand.Api.Core.Requests;

namespace RacksStand.Api.Features.Suppliers;

public record SupplierRequest
(
    string Name,
    string Code,
    string? PrefererCommunication,
    string? Description,
    string? RepresentiveName,
    AddressRequest Address,
    AddressRequest BillingAddress
);
