using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Repositories;

namespace RacksStand.Api.Features.Products.Repositories;
public interface IProductRepository : IRepository<Product, ProductId>
{
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(CategoryId categoryId, CancellationToken cancellationToken);
    Task<Product?> GetProductWithDetailsAsync(ProductId productId, CancellationToken cancellationToken);
}

