using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Features.Categories.Domain.ValueObjects;
using RacksStand.Api.Features.Products.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;

namespace RacksStand.Api.Features.Categories.Domain.Entities;
public class Category : Entity<CategoryId>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required ShopId ShopId { get; set; }

    // Navigation properties
    public virtual Shop Store { get; set; } = default!;
    public virtual ICollection<Product> Products { get; set; } = [];
}
