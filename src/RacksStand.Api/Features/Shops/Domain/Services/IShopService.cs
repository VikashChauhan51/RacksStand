using RacksStand.Models;

namespace RacksStand.Api.Features.Shops.Domain.Services;

public interface IShopService
{
    Task<Result<bool>> CheckIfShopExistsAsync(string name, string ownerId, CancellationToken cancellationToken = default);
    Task<Result<string>> AddShopAsync(ShopModel model, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteShopAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<ShopModel>> GetShopAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<PaginatedResult<ShopModel>>> GetShopsAsync(int pageSize, int pageNumber, string userId, string? filter, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateShopAsync(string id, ShopModel model, CancellationToken cancellationToken = default);
}
