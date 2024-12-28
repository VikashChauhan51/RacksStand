
namespace RacksStand.UnifiedServices.DataServices;

public interface IProductService
{
    Task<Result<string>> AddProductAsync(ProductModel model, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteProductAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<ProductModel>> GetProductAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateProductAsync(string id, ProductModel model, CancellationToken cancellationToken = default);
}