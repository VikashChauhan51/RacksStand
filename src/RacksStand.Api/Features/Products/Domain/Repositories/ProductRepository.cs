using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Categories.Domain.ValueObjects;
using RacksStand.Api.Features.Products.Domain.Entities;
using RacksStand.Api.Features.Products.Domain.ValueObjects;

namespace RacksStand.Api.Features.Products.Domain.Repositories;
public class ProductRepository : Repository<Product, ProductId>, IProductRepository
{
    public ProductRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryIdAsync(CategoryId categoryId, CancellationToken cancellationToken)
    {
        return await Context.Set<Product>()
                            .Where(p => p.CategoryId == categoryId)
                            .ToListAsync(cancellationToken);
    }

    public async Task<Product?> GetProductWithDetailsAsync(ProductId productId, CancellationToken cancellationToken)
    {
        return await Context.Set<Product>()
                            .Include(p => p.Category)
                            .Include(p => p.Supplier)
                            .FirstOrDefaultAsync(p => p.Id == productId, cancellationToken);
    }
}
