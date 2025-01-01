using RacksStand.Domain.Core;
using RacksStand.Domain.ValueObjects;

namespace RacksStand.Domain.Entities;
public class Customer : Entity<CustomerId>
{
    public required string FullName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public required string AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public required string State { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public required string ZipCode { get; set; }
    public string? Notes { get; set; }

    public virtual ICollection<SaleOrder> SaleOrders { get; set; } = [];

}

