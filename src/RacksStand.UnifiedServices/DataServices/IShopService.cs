
namespace RacksStand.UnifiedServices.DataServices;

public interface IShopService
{
    Task<Result<string>> AddShopAsync(ShopModel model, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteShopAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<ShopModel>> GetShopAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<PaginatedResult<ShopModel>>> GetShopsAsync(int pageSize, int pageNumber, string? filter, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateShopAsync(string id, ShopModel model, CancellationToken cancellationToken = default);
}