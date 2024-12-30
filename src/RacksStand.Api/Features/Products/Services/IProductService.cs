using RacksStand.Models;

namespace RacksStand.Api.Features.Products.Services;

public interface IProductService
{
    Task<Result<bool>> CheckIfProductExistsAsync(string name, string categoryId, CancellationToken cancellationToken = default);
    Task<Result<string>> AddProductAsync(ProductModel model, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteProductAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<ProductModel>> GetProductAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<PaginatedResult<ProductModel>>> GetProductsAsync(string categoryId, int pageSize, int pageNumber, string? filter, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateProductAsync(string id, ProductModel model, CancellationToken cancellationToken = default);
}
