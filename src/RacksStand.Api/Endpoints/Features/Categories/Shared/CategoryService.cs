using RacksStand.Persistence.Entities;
using RacksStand.Persistence.Exceptions;
using RacksStand.Persistence.ValueObjects;
using RacksStand.Models;
using RacksStand.Persistence.Repositories;



namespace RacksStand.Api.Endpoints.Features.Categories.Shared;

public sealed class CategoryService : ICategoryService
{
    private readonly IRepository<Category> _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(IRepository<Category> categoryRepository, IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> AddCategoryAsync(CategoryModel model, CancellationToken cancellationToken = default)
    {
        if (model == null)
        {
            _logger.LogWarning("The category request model can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(model)));
        }

        var category = new Category
        {
            Id = CategoryId.Of(Guid.NewGuid().ToString()),
            Name = model.Name,
            ShopId = ShopId.Of(model.ShopId),
            Description = model.Description,
            CreatedAt = DateTime.UtcNow,
        };

        await _categoryRepository.AddAsync(category, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succ(category.Id.Value);
    }

    public async Task<Result<CategoryModel>> GetCategoryAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The category ID can't be null.");
            return Result<CategoryModel>.Fail(new ArgumentNullException(nameof(id)));
        }

        var response = await _categoryRepository.GetByIdAsync<string>(id, cancellationToken);

        if (response == null)
        {
            _logger.LogWarning("The category for ID:{Id} is not found.", id);
            return Result<CategoryModel>.Fail(new EntityNotFoundException(nameof(id)));
        }

        return Result<CategoryModel>.Succ(response.Adapt<CategoryModel>());
    }

    public async Task<Result<PaginatedResult<CategoryModel>>> GetCategoriesAsync(string shopId, int pageSize, int pageNumber, string? filter, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(shopId))
        {
            _logger.LogWarning("The shop ID can't be null.");
            return Result<PaginatedResult<CategoryModel>>.Fail(new ArgumentException("The shop ID can't be null.", nameof(shopId)));
        }
        if (pageNumber <= 0)
        {
            _logger.LogWarning("Page number must be greater than zero.");
            return Result<PaginatedResult<CategoryModel>>.Fail(new ArgumentException("Page number must be greater than zero.", nameof(pageNumber)));
        }
        if (pageSize <= 0)
        {
            _logger.LogWarning("Page size must be greater than zero.");
            return Result<PaginatedResult<CategoryModel>>.Fail(new ArgumentException("Page size must be greater than zero.", nameof(pageSize)));
        }

        var response = await _categoryRepository.GetPaginatedAsync(
            pageNumber,
            pageSize,
            true,
            x => x.ShopId.Value == shopId &&
            (string.IsNullOrEmpty(filter) || x.Name.StartsWith(filter)),
            null,
            cancellationToken
        );

        if (response == null)
        {
            _logger.LogWarning("The category not exists.");
            return Result<PaginatedResult<CategoryModel>>.Fail(new EntityNotFoundException("The category not exists"));
        }

        return Result<PaginatedResult<CategoryModel>>.Succ(new PaginatedResult<CategoryModel>
        {
            Items = response.Items.Select(x => x.Adapt<CategoryModel>()),
            TotalItems = response.TotalItems,
            PageNumber = response.PageNumber,
            PageSize = response.PageSize
        });
    }

    public async Task<Result<string>> UpdateCategoryAsync(string id, CategoryModel model, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The category ID can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(id)));
        }

        var existingCategory = await _categoryRepository.GetByIdAsync<string>(id, cancellationToken);

        if (existingCategory == null)
        {
            _logger.LogWarning("The category for ID:{Id} is not found.", id);
            return Result<string>.Fail(new EntityNotFoundException(nameof(id)));
        }

        existingCategory.Name = model.Name;
        existingCategory.Description = model.Description;
        existingCategory.UpdatedAt = DateTime.UtcNow;

        _categoryRepository.Update(existingCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succ(id);
    }

    public async Task<Result<string>> DeleteCategoryAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The category ID can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(id)));
        }

        var existingCategory = await _categoryRepository.GetByIdAsync<string>(id, cancellationToken);

        if (existingCategory == null)
        {
            _logger.LogWarning("The category for ID:{Id} is not found.", id);
            return Result<string>.Fail(new EntityNotFoundException(nameof(id)));
        }

        existingCategory.IsActive = false;
        existingCategory.UpdatedAt = DateTime.UtcNow;

        _categoryRepository.Update(existingCategory);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succ(id);
    }
}

