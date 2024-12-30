using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.Exceptions;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Infrastructure.Repositories;
using RacksStand.Models;

namespace RacksStand.Api.Core.Infrastructure.Services;
public sealed class SupplierService : ISupplierService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SupplierService> _logger;

    public SupplierService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork, ILogger<SupplierService> logger)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Result<bool>> CheckIfSupplierExistsAsync(string name, string shopId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Supplier name cannot be null or empty.");
            return Result<bool>.Failure(new ArgumentNullException(nameof(name)));
        }

        if (string.IsNullOrWhiteSpace(shopId))
        {
            _logger.LogWarning("Shop ID cannot be null or empty.");
            return Result<bool>.Failure(new ArgumentNullException(nameof(shopId)));
        }

        var normalizedName = name.Trim().ToLowerInvariant();
        var exists = await _supplierRepository
            .GetAll()
            .Where(x => x.Name.ToLower() == normalizedName && x.ShopId == ShopId.Of(shopId))
            .AnyAsync(cancellationToken);

        if (exists)
        {
            _logger.LogInformation("A Supplier with the name '{SupplierName}' already exists for shop '{ShopId}'.", name, shopId);
        }
        else
        {
            _logger.LogInformation("No Supplier found with the name '{SupplierName}' for shop '{ShopId}'.", name, shopId);
        }

        return Result<bool>.Success(exists);
    }

    public async Task<Result<string>> AddSupplierAsync(SupplierModel model, CancellationToken cancellationToken = default)
    {
        if (model == null)
        {
            _logger.LogWarning("The supplier request model can't be null.");
            return Result<string>.Failure(new ArgumentNullException(nameof(model)));
        }

        var exists = await CheckIfSupplierExistsAsync(model.Name, model.ShopId, cancellationToken);

        if (exists.Value)
        {
            _logger.LogWarning("A Supplier with the same name already exists: {SupplierName}", model.Name);
            return Result<string>.Failure(new DuplicateEntityException($"A Supplier with the name '{model.Name}' already exists."));
        }

        var supplier = new Supplier
        {
            Id = SupplierId.Of(Guid.NewGuid().ToString()),
            Name = model.Name,
            Code = model.Code,
            ShopId = ShopId.Of(model.ShopId),
            CreatedAt = DateTime.UtcNow,
        };

        await _supplierRepository.AddAsync(supplier, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Success(supplier.Id.Value);
    }

    public async Task<Result<SupplierModel>> GetSupplierAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The supplier ID can't be null.");
            return Result<SupplierModel>.Failure(new ArgumentNullException(nameof(id)));
        }

        var response = await _supplierRepository.GetByIdAsync(SupplierId.Of(id), cancellationToken);

        if (response == null || !response.IsActive)
        {
            _logger.LogWarning("The supplier for ID:{Id} is not found.", id);
            return Result<SupplierModel>.Failure(new EntityNotFoundException(nameof(id)));
        }

        return Result<SupplierModel>.Success(response.Adapt<SupplierModel>());
    }

    public async Task<Result<PaginatedResult<SupplierModel>>> GetSuppliersAsync(string shopId, int pageSize, int pageNumber, string? filter, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(shopId))
        {
            _logger.LogWarning("The shop ID can't be null.");
            return Result<PaginatedResult<SupplierModel>>.Failure(new ArgumentException("The shop ID can't be null.", nameof(shopId)));
        }
        if (pageNumber <= 0)
        {
            _logger.LogWarning("Page number must be greater than zero.");
            return Result<PaginatedResult<SupplierModel>>.Failure(new ArgumentException("Page number must be greater than zero.", nameof(pageNumber)));
        }
        if (pageSize <= 0)
        {
            _logger.LogWarning("Page size must be greater than zero.");
            return Result<PaginatedResult<SupplierModel>>.Failure(new ArgumentException("Page size must be greater than zero.", nameof(pageSize)));
        }

        var response = await _supplierRepository.GetPaginatedAsync(
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
            _logger.LogWarning("No suppliers found.");
            return Result<PaginatedResult<SupplierModel>>.Failure(new EntityNotFoundException("No suppliers found."));
        }

        return Result<PaginatedResult<SupplierModel>>.Success(new PaginatedResult<SupplierModel>
        {
            Items = response.Items.Select(x => x.Adapt<SupplierModel>()),
            TotalItems = response.TotalItems,
            PageNumber = response.PageNumber,
            PageSize = response.PageSize
        });
    }

    public async Task<Result<string>> UpdateSupplierAsync(string id, SupplierModel model, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The supplier ID can't be null.");
            return Result<string>.Failure(new ArgumentNullException(nameof(id)));
        }

        var existingSupplier = await _supplierRepository.GetByIdAsync(SupplierId.Of(id), cancellationToken);

        if (existingSupplier == null || !existingSupplier.IsActive)
        {
            _logger.LogWarning("The supplier for ID:{Id} is not found.", id);
            return Result<string>.Failure(new EntityNotFoundException(nameof(id)));
        }

        if (existingSupplier.Name != model.Name)
        {
            var exists = await CheckIfSupplierExistsAsync(model.Name, model.ShopId, cancellationToken);

            if (exists.Value)
            {
                _logger.LogWarning("A Supplier with the same name already exists: {SupplierName}", model.Name);
                return Result<string>.Failure(new DuplicateEntityException($"A Supplier with the name '{model.Name}' already exists."));
            }
        }

        existingSupplier.Name = model.Name;
        existingSupplier.Code = model.Code;
        existingSupplier.UpdatedAt = DateTime.UtcNow;

        _supplierRepository.Update(existingSupplier);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(id);
    }

    public async Task<Result<string>> DeleteSupplierAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The supplier ID can't be null.");
            return Result<string>.Failure(new ArgumentNullException(nameof(id)));
        }

        var existingSupplier = await _supplierRepository.GetByIdAsync(SupplierId.Of(id), cancellationToken);

        if (existingSupplier == null || !existingSupplier.IsActive)
        {
            _logger.LogWarning("The supplier for ID:{Id} is not found.", id);
            return Result<string>.Failure(new EntityNotFoundException(nameof(id)));
        }

        existingSupplier.IsActive = false;
        existingSupplier.UpdatedAt = DateTime.UtcNow;

        _supplierRepository.Update(existingSupplier);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Success(id);
    }
}

