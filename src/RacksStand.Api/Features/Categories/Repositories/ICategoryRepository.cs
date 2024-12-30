using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Infrastructure.Repositories;

namespace RacksStand.Api.Features.Categories.Repositories;
public interface ICategoryRepository : IRepository<Category, CategoryId>
{
    Task<IEnumerable<Category>> GetCategoriesByShopIdAsync(ShopId shopId, CancellationToken cancellationToken);
}

