using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.Exceptions;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Enums;
using RacksStand.Api.Core.Infrastructure.Repositories;
using RacksStand.Models;

namespace RacksStand.Api.Core.Infrastructure.Services;
public sealed class ShopService : IShopService
{
    private readonly IShopRepository _shopRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ShopService> _logger;

    public ShopService
    (
        IShopRepository shopRepository,
        IUnitOfWork unitOfWork,
        IUserShopRepository userShopRepository,
        ILogger<ShopService> logger)
    {
        _shopRepository = shopRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }


    public async Task<Result<bool>> CheckIfShopExistsAsync(string name, string ownerId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Shop name cannot be null or empty.");
            return Result<bool>.Fail(new ArgumentNullException(nameof(name)));
        }

        if (string.IsNullOrWhiteSpace(ownerId))
        {
            _logger.LogWarning("Owner ID cannot be null or empty.");
            return Result<bool>.Fail(new ArgumentNullException(nameof(ownerId)));
        }

        var normalizedName = name.Trim().ToLowerInvariant();
        var exists = await _shopRepository
            .GetAll()
            .Where(x => x.Name.ToLower() == normalizedName && x.OwnerId == ownerId)
            .AnyAsync(cancellationToken);

        if (exists)
        {
            _logger.LogInformation("A shop with the name '{ShopName}' already exists for owner '{OwnerId}'.", name, ownerId);
        }
        else
        {
            _logger.LogInformation("No shop found with the name '{ShopName}' for owner '{OwnerId}'.", name, ownerId);
        }

        return Result<bool>.Succ(exists);
    }
    public async Task<Result<string>> AddShopAsync(ShopModel model, CancellationToken cancellationToken = default)
    {
        if (model == null)
        {
            _logger.LogWarning("The shop request model can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(model)));
        }

        var exists = await CheckIfShopExistsAsync(model.Name, model.OwnerId, cancellationToken);

        if (exists.Value)
        {
            _logger.LogWarning("A shop with the same name already exists: {ShopName}", model.Name);
            return Result<string>.Fail(new DuplicateEntityException($"A shop with the name '{model.Name}' already exists."));
        }
        var shop = new Shop
        {
            Id = ShopId.Of(Guid.NewGuid().ToString()),
            Name = model.Name,
            Address = model.Address,
            OwnerId = model.OwnerId,
            Description = model.Description,
            CreatedAt = DateTime.UtcNow,
        };

        var userShop = new UserShop
        {
            Id = UserShopId.Of(Guid.NewGuid().ToString()),
            StoreId = shop.Id,
            UserId = shop.OwnerId,
            Role = UserRole.Owner,
            CreatedAt = DateTime.UtcNow,
            IsActive = true

        };
        shop.UserStores.Add(userShop);

        try
        {
            await _unitOfWork.BeginTransactionAsync(cancellationToken);
            await _shopRepository.AddAsync(shop, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync(cancellationToken);
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync(cancellationToken);
            throw;
        }

        return Result<string>.Succ(shop.Id.Value);
    }
    public async Task<Result<ShopModel>> GetShopAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The shop ID can't be null.");
            return Result<ShopModel>.Fail(new ArgumentNullException(nameof(id)));
        }

        var response = await _shopRepository.GetByIdAsync(ShopId.Of(id), cancellationToken);

        if (response == null || !response.IsActive)
        {
            _logger.LogWarning("The shop for ID:{Id} is not found.", id);
            return Result<ShopModel>.Fail(new EntityNotFoundException(nameof(id)));
        }

        return Result<ShopModel>.Succ(response.Adapt<ShopModel>());
    }

    public async Task<Result<PaginatedResult<ShopModel>>> GetShopsAsync(int pageSize, int pageNumber, string userId, string? filter, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(userId))
        {
            _logger.LogWarning("User Id can't be null.");
            return Result<PaginatedResult<ShopModel>>.Fail(new ArgumentException("User Id can't be null.", nameof(userId)));
        }

        if (pageNumber <= 0)
        {
            _logger.LogWarning("Page number must be greater than zero.");
            return Result<PaginatedResult<ShopModel>>.Fail(new ArgumentException("Page number must be greater than zero.", nameof(pageNumber)));
        }
        if (pageSize <= 0)
        {
            _logger.LogWarning("Page size must be greater than zero.");
            return Result<PaginatedResult<ShopModel>>.Fail(new ArgumentException("Page size must be greater than zero.", nameof(pageSize)));
        }

        var query = _shopRepository
            .GetAll()
            .Include(x => x.UserStores)
            .Where(x => x.UserStores.Any(s => s.UserId == userId && s.IsActive) &&
            x.IsActive &&
            (string.IsNullOrEmpty(filter) || x.Name.StartsWith(filter))
            );

        var totalItems = await query.CountAsync(cancellationToken);
        var response = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        if (response == null)
        {
            _logger.LogWarning("No shops found.");
            return Result<PaginatedResult<ShopModel>>.Fail(new EntityNotFoundException("No shops found."));
        }

        return Result<PaginatedResult<ShopModel>>.Succ(new PaginatedResult<ShopModel>
        {
            Items = response.Select(x => x.Adapt<ShopModel>()),
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize
        });
    }

    public async Task<Result<string>> UpdateShopAsync(string id, ShopModel model, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The shop ID can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(id)));
        }

        var existingShop = await _shopRepository.GetByIdAsync(ShopId.Of(id), cancellationToken);

        if (existingShop == null || !existingShop.IsActive)
        {
            _logger.LogWarning("The shop for ID:{Id} is not found.", id);
            return Result<string>.Fail(new EntityNotFoundException(nameof(id)));
        }

        if (existingShop.Name != model.Name)
        {
            var exists = await CheckIfShopExistsAsync(model.Name, model.OwnerId, cancellationToken);

            if (exists.Value)
            {
                _logger.LogWarning("A shop with the same name already exists: {ShopName}", model.Name);
                return Result<string>.Fail(new DuplicateEntityException($"A shop with the name '{model.Name}' already exists."));
            }
        }

        existingShop.Name = model.Name;
        existingShop.Address = model.Address;
        existingShop.Description = model.Description;
        existingShop.UpdatedAt = DateTime.UtcNow;

        _shopRepository.Update(existingShop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succ(id);
    }

    public async Task<Result<string>> DeleteShopAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The shop ID can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(id)));
        }

        var existingShop = await _shopRepository.GetByIdAsync(ShopId.Of(id), cancellationToken);

        if (existingShop == null || !existingShop.IsActive)
        {
            _logger.LogWarning("The shop for ID:{Id} is not found.", id);
            return Result<string>.Fail(new EntityNotFoundException(nameof(id)));
        }

        existingShop.IsActive = false;
        existingShop.UpdatedAt = DateTime.UtcNow;

        _shopRepository.Update(existingShop);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succ(id);
    }
}
