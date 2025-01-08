using RacksStand.Api.Core.Requests;

namespace RacksStand.Api.Features.Warehouses;

public record WarehouseRequest
(
    string ManagerId,
    string Name,
    string Code,
    string Description,
    string PhoneNumber,
    decimal Height,
    decimal Width,
    decimal Length,
    string DimensionUnit,
    AddressRequest Address,
    AddressRequest ReturnAddress
);
