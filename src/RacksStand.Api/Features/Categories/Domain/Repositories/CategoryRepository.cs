using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Categories.Domain.Entities;
using RacksStand.Api.Features.Categories.Domain.ValueObjects;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;

namespace RacksStand.Api.Features.Categories.Domain.Repositories;
public class CategoryRepository : Repository<Category, CategoryId>, ICategoryRepository
{
    public CategoryRepository(DbContext context) : base(context)
    {

    }

    public async Task<IEnumerable<Category>> GetCategoriesByShopIdAsync(ShopId shopId, CancellationToken cancellationToken)
    {
        return await Context.Set<Category>()
                            .Where(c => c.ShopId == shopId)
                            .ToListAsync(cancellationToken);
    }
}
