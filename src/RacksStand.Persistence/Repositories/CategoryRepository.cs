using Microsoft.EntityFrameworkCore;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Persistence.Repositories;
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
