using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Infrastructure.Repositories;
using RacksStand.Api.Features.Products.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Products.Repositories;
public class ProductRepository : Repository<Product, ProductId>, IProductRepository
{
    public ProductRepository(ProductContext context) : base(context)
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
