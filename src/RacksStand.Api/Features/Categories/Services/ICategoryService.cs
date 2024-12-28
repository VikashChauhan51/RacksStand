using RacksStand.Models;

namespace RacksStand.Api.Features.Categories.Services;

public interface ICategoryService
{
    Task<Result<bool>> CheckIfCategoryExistsAsync(string name, string shopId, CancellationToken cancellationToken = default);
    Task<Result<string>> AddCategoryAsync(CategoryModel model, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteCategoryAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<PaginatedResult<CategoryModel>>> GetCategoriesAsync(string shopId, int pageSize, int pageNumber, string? filter, CancellationToken cancellationToken = default);
    Task<Result<CategoryModel>> GetCategoryAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateCategoryAsync(string id, CategoryModel model, CancellationToken cancellationToken = default);
}
