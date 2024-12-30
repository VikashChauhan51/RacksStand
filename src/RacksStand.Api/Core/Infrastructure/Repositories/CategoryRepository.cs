using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
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
