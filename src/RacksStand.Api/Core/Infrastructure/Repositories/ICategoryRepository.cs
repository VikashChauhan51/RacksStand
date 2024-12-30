using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;

namespace RacksStand.Api.Core.Infrastructure.Repositories;
public interface ICategoryRepository : IRepository<Category, CategoryId>
{
    Task<IEnumerable<Category>> GetCategoriesByShopIdAsync(ShopId shopId, CancellationToken cancellationToken);
}

