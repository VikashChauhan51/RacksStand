namespace RacksStand.Api.Features.Companies;

public record GetCompanyResponse
(
    string Id,
    string Name,
    string Description,
    string AddressLine1,
    string? AddressLine2,
    string State,
    string City,
    string Country,
    string ZipCode,
    string DefaultDimensionUnit,
    string DefaultWeightUnit,
    string DefaultCurrency,
    string DefaultItemBarcodeFormat,
    string DefaultBinBarcodeFormat,
    string DefaultShipmentBarcodeFormat,
    bool IsOwner
);
