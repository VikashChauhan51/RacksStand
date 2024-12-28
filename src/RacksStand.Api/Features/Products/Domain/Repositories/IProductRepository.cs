using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Categories.Domain.ValueObjects;
using RacksStand.Api.Features.Products.Domain.Entities;
using RacksStand.Api.Features.Products.Domain.ValueObjects;

namespace RacksStand.Api.Features.Products.Domain.Repositories;
public interface IProductRepository : IRepository<Product, ProductId>
{
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(CategoryId categoryId, CancellationToken cancellationToken);
    Task<Product?> GetProductWithDetailsAsync(ProductId productId, CancellationToken cancellationToken);
}

