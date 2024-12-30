using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
public interface IProductRepository : IRepository<Product, ProductId>
{
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(CategoryId categoryId, CancellationToken cancellationToken);
    Task<Product?> GetProductWithDetailsAsync(ProductId productId, CancellationToken cancellationToken);
}

