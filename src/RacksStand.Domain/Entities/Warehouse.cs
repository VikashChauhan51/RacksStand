using RacksStand.Domain.Core;
using RacksStand.Domain.Enums;
using RacksStand.Domain.ValueObjects;


namespace RacksStand.Domain.Entities;
public class Warehouse : Entity<WarehouseId>
{
    public required CompanyId CompanyId { get; set; }
    public required UserId ManagerId { get; set; }
    public required string Name { get; set; }
    public required string Prefix { get; set; }
    public required string Description { get; set; }
    public required string PhoneNumber { get; set; }
    public required Height Height { get; set; }
    public required Width Width { get; set; }
    public required Length Length { get; set; }
    public DimensionUnit DiminsionUnit { get; set; }
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string ZipCode { get; set; }
    public required string ReturnAddressLine1 { get; set; }
    public string? ReturnAddressLine2 { get; set; }
    public required string ReturnState { get; set; }
    public required string ReturnCity { get; set; }
    public required string ReturnCountry { get; set; }
    public required string ReturnZipCode { get; set; }
}
