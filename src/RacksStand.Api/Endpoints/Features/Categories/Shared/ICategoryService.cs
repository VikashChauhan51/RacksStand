using RacksStand.Models;

namespace RacksStand.Api.Endpoints.Features.Categories.Shared;
public interface ICategoryService
{
    Task<Result<string>> AddCategoryAsync(CategoryModel model, CancellationToken cancellationToken = default);
    Task<Result<PaginatedResult<CategoryModel>>> GetCategoriesAsync(int pageSize, int pageNumber, string? filter, CancellationToken cancellationToken = default);
    Task<Result<CategoryModel>> GetCategoryAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<string>> SoftDeleteCategoryAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateCategoryAsync(string id, CategoryModel model, CancellationToken cancellationToken = default);
}