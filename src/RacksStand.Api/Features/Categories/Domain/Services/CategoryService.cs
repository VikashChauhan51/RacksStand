using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Exceptions;
using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Categories.Domain.Entities;
using RacksStand.Api.Features.Categories.Domain.Repositories;
using RacksStand.Api.Features.Categories.Domain.ValueObjects;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;
using RacksStand.Models;

namespace RacksStand.Api.Features.Categories.Domain.Services;

public sealed class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CategoryService> _logger;

    public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<bool>> CheckIfCategoryExistsAsync(string name, string shopId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Category name cannot be null or empty.");
            return Result<bool>.Fail(new ArgumentNullException(nameof(name)));
        }

        if (string.IsNullOrWhiteSpace(shopId))
        {
            _logger.LogWarning("Shop ID cannot be null or empty.");
            return Result<bool>.Fail(new ArgumentNullException(nameof(shopId)));
        }

        var normalizedName = name.Trim().ToLowerInvariant();
        var exists = await _categoryRepository
            .GetAll()
            .Where(x => x.Name.ToLower() == normalizedName && x.ShopId == ShopId.Of(shopId))
            .AnyAsync(cancellationToken);

        if (exists)
        {
            _logger.LogInformation("A Category with the name '{CategoryName}' already exists for owner '{ShopId}'.", name, shopId);
        }
        else
        {
            _logger.LogInformation("No Category found with the name '{CategoryName}' for owner '{ShopId}'.", name, shopId);
        }

        return Result<bool>.Succ(exists);
    }
    public async Task<Result<string>> AddCategoryAsync(CategoryModel model, CancellationToken cancellationToken = default)
    {
        if (model == null)
        {
            _logger.LogWarning("The category request model can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(model)));
        }

        var exists = await CheckIfCategoryExistsAsync(model.Name, model.ShopId, cancellationToken);

        if (exists.Value)
        {
            _logger.LogWarning("A Category with the same name already exists: {CategoryName}", model.Name);
            return Result<string>.Fail(new DuplicateEntityException($"A Category with the name '{model.Name}' already exists."));
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

        var response = await _categoryRepository.GetByIdAsync(CategoryId.Of(id), cancellationToken);

        if (response == null || !response.IsActive)
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
            x => x.ShopId == ShopId.Of(shopId) &&
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

        var existingCategory = await _categoryRepository.GetByIdAsync(CategoryId.Of(id), cancellationToken);

        if (existingCategory == null || !existingCategory.IsActive)
        {
            _logger.LogWarning("The category for ID:{Id} is not found.", id);
            return Result<string>.Fail(new EntityNotFoundException(nameof(id)));
        }

        if (existingCategory.Name != model.Name)
        {
            var exists = await CheckIfCategoryExistsAsync(model.Name, model.ShopId, cancellationToken);

            if (exists.Value)
            {
                _logger.LogWarning("A Category with the same name already exists: {CategoryName}", model.Name);
                return Result<string>.Fail(new DuplicateEntityException($"A Category with the name '{model.Name}' already exists."));
            }
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

        var existingCategory = await _categoryRepository.GetByIdAsync(CategoryId.Of(id), cancellationToken);

        if (existingCategory == null || !existingCategory.IsActive)
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
