using RacksStand.Api.Endpoints.Features.Shops.CreateShop;
using RacksStand.Domain.Entities;
using RacksStand.Domain.Exceptions;
using RacksStand.Domain.ValueObjects;
using RacksStand.Models;
using RacksStand.Persistence.Repositories;

namespace RacksStand.Api.Endpoints.Features.Shops.Shared;

public sealed class ShopService : IShopService
{
    private readonly IRepository<Shop> _shopRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ShopService> _logger;

    public ShopService(IRepository<Shop> shopRepository, IUnitOfWork unitOfWork, ILogger<ShopService> logger)
    {
        _shopRepository = shopRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<string>> AddShopAsync(ShopModel model, CancellationToken cancellationToken = default)
    {
        if (model == null)
        {
            _logger.LogWarning("The shop request model can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(model)));
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

        await _shopRepository.AddAsync(shop, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succ(shop.Id.Value);
    }

    public async Task<Result<ShopModel>> GetShopAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The shop ID can't be null.");
            return Result<ShopModel>.Fail(new ArgumentNullException(nameof(id)));
        }

        var response = await _shopRepository.GetByIdAsync<string>(id, cancellationToken);

        if (response == null)
        {
            _logger.LogWarning("The shop for ID:{Id} is not found.", id);
            return Result<ShopModel>.Fail(new EntityNotFoundException(nameof(id)));
        }

        return Result<ShopModel>.Succ(response.Adapt<ShopModel>());
    }

    public async Task<Result<PaginatedResult<ShopModel>>> GetShopsAsync(int pageSize, int pageNumber, string? filter, CancellationToken cancellationToken = default)
    {
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

        var response = await _shopRepository.GetPaginatedAsync(
            pageNumber,
            pageSize,
            true,
            x => x.OwnerId == "123" &&
            (string.IsNullOrEmpty(filter) || x.Name.StartsWith(filter)), //TODO: update with actual user ID.
            null,
            cancellationToken
        );

        if (response == null)
        {
            _logger.LogWarning("No shops found.");
            return Result<PaginatedResult<ShopModel>>.Fail(new EntityNotFoundException("No shops found."));
        }

        return Result<PaginatedResult<ShopModel>>.Succ(new PaginatedResult<ShopModel>
        {
            Items = response.Items.Select(x => x.Adapt<ShopModel>()),
            TotalItems = response.TotalItems,
            PageNumber = response.PageNumber,
            PageSize = response.PageSize
        });
    }

    public async Task<Result<string>> UpdateShopAsync(string id, ShopModel model, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The shop ID can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(id)));
        }

        var existingShop = await _shopRepository.GetByIdAsync<string>(id, cancellationToken);

        if (existingShop == null)
        {
            _logger.LogWarning("The shop for ID:{Id} is not found.", id);
            return Result<string>.Fail(new EntityNotFoundException(nameof(id)));
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

        var existingShop = await _shopRepository.GetByIdAsync<string>(id, cancellationToken);

        if (existingShop == null)
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

