using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Infrastructure.Data.Contexts;
using RacksStand.Api.Core.Infrastructure.Repositories;

namespace RacksStand.Api.Features.Categories.Repositories;
public class CategoryRepository : Repository<Category, CategoryId>, ICategoryRepository
{
    public CategoryRepository(RacksStandContext context) : base(context)
    {

    }

    public async Task<IEnumerable<Category>> GetCategoriesByShopIdAsync(ShopId shopId, CancellationToken cancellationToken)
    {
        return await Context.Set<Category>()
                            .Where(c => c.ShopId == shopId)
                            .ToListAsync(cancellationToken);
    }
}
