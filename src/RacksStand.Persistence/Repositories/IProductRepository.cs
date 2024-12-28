using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;
namespace RacksStand.Persistence.Repositories;
public interface IProductRepository : IRepository<Product, ProductId>
{
    Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(CategoryId categoryId, CancellationToken cancellationToken);
    Task<Product?> GetProductWithDetailsAsync(ProductId productId, CancellationToken cancellationToken);
}

