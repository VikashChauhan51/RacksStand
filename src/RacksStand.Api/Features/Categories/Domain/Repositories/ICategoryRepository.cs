using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Categories.Domain.Entities;
using RacksStand.Api.Features.Categories.Domain.ValueObjects;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;

namespace RacksStand.Api.Features.Categories.Domain.Repositories;
public interface ICategoryRepository : IRepository<Category, CategoryId>
{
    Task<IEnumerable<Category>> GetCategoriesByShopIdAsync(ShopId shopId, CancellationToken cancellationToken);
}

