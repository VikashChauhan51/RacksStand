using Microsoft.EntityFrameworkCore;

namespace RacksStand.UnifiedServices.DataServices;
public sealed class ProductService : IProductService
{
    private readonly IRepository<Product> _productRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IRepository<Product> productRepository, IUnitOfWork unitOfWork, ILogger<ProductService> logger)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    // Check if a product already exists with the same categoryId and name
    private async Task<Result<bool>> CheckIfProductExistsAsync(string name, string categoryId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            _logger.LogWarning("Product name cannot be null or empty.");
            return Result<bool>.Fail(new ArgumentNullException(nameof(name)));
        }

        if (string.IsNullOrWhiteSpace(categoryId))
        {
            _logger.LogWarning("Category ID cannot be null or empty.");
            return Result<bool>.Fail(new ArgumentNullException(nameof(categoryId)));
        }

        var exists = await _productRepository
            .GetAll()
            .Where(x => x.Name.ToLower() == name.ToLower() && x.CategoryId == CategoryId.Of(categoryId))
            .AnyAsync(cancellationToken);

        if (exists)
        {
            _logger.LogInformation("A Product with the name '{ProductName}' already exists under category '{CategoryId}'.", name, categoryId);
        }
        else
        {
            _logger.LogInformation("No Product found with the name '{ProductName}' under category '{CategoryId}'.", name, categoryId);
        }

        return Result<bool>.Succ(exists);
    }

    // Add new product
    public async Task<Result<string>> AddProductAsync(ProductModel model, CancellationToken cancellationToken = default)
    {
        if (model == null)
        {
            _logger.LogWarning("The product request model can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(model)));
        }

        var exists = await CheckIfProductExistsAsync(model.Name, model.CategoryId, cancellationToken);

        if (exists.Value)
        {
            _logger.LogWarning("A Product with the same name already exists in this category: {ProductName}", model.Name);
            return Result<string>.Fail(new DuplicateEntityException($"A Product with the name '{model.Name}' already exists in this category."));
        }

        var product = new Product
        {
            Id = ProductId.Of(Guid.NewGuid().ToString()),
            Name = model.Name,
            Description = model.Description,
            CategoryId = CategoryId.Of(model.CategoryId),
            SupplierId = model.SupplierId != null ? SupplierId.Of(model.SupplierId) : null,
            Code = model.Code,
            Sku = model.Sku,
            PurchasePrice = PurchasePrice.Of(model.PurchasePrice),
            SellingPrice = SellingPrice.Of(model.SellingPrice),
            Unit = Unit.Of(model.Unit),
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<string>.Succ(product.Id.Value);
    }

    // Get product by Id
    public async Task<Result<ProductModel>> GetProductAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The product ID can't be null or empty.");
            return Result<ProductModel>.Fail(new ArgumentNullException(nameof(id)));
        }

        var response = await _productRepository.GetByIdAsync<ProductId>(ProductId.Of(id), cancellationToken);

        if (response == null || !response.IsActive)
        {
            _logger.LogWarning("The product for ID:{Id} is not found.", id);
            return Result<ProductModel>.Fail(new EntityNotFoundException(nameof(id)));
        }

        return Result<ProductModel>.Succ(response.Adapt<ProductModel>());
    }

    // Update product
    public async Task<Result<string>> UpdateProductAsync(string id, ProductModel model, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The product ID can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(id)));
        }

        var existingProduct = await _productRepository.GetByIdAsync<ProductId>(ProductId.Of(id), cancellationToken);

        if (existingProduct == null || !existingProduct.IsActive)
        {
            _logger.LogWarning("The product for ID:{Id} is not found.", id);
            return Result<string>.Fail(new EntityNotFoundException(nameof(id)));
        }

        if (existingProduct.Name != model.Name || existingProduct.CategoryId != CategoryId.Of(model.CategoryId))
        {
            var exists = await CheckIfProductExistsAsync(model.Name, model.CategoryId, cancellationToken);

            if (exists.Value)
            {
                _logger.LogWarning("A Product with the same name already exists in this category: {ProductName}", model.Name);
                return Result<string>.Fail(new DuplicateEntityException($"A Product with the name '{model.Name}' already exists in this category."));
            }
        }

        existingProduct.Name = model.Name;
        existingProduct.Description = model.Description;
        existingProduct.Code = model.Code;
        existingProduct.Sku = model.Sku;
        existingProduct.PurchasePrice = PurchasePrice.Of(model.PurchasePrice);
        existingProduct.SellingPrice = SellingPrice.Of(model.SellingPrice);
        existingProduct.Unit = Unit.Of(model.Unit);
        existingProduct.UpdatedAt = DateTime.UtcNow;

        _productRepository.Update(existingProduct);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succ(id);
    }

    // Delete product
    public async Task<Result<string>> DeleteProductAsync(string id, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(id))
        {
            _logger.LogWarning("The product ID can't be null.");
            return Result<string>.Fail(new ArgumentNullException(nameof(id)));
        }

        var existingProduct = await _productRepository.GetByIdAsync<ProductId>(ProductId.Of(id), cancellationToken);

        if (existingProduct == null || !existingProduct.IsActive)
        {
            _logger.LogWarning("The product for ID:{Id} is not found.", id);
            return Result<string>.Fail(new EntityNotFoundException(nameof(id)));
        }

        existingProduct.IsActive = false;
        existingProduct.UpdatedAt = DateTime.UtcNow;

        _productRepository.Update(existingProduct);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<string>.Succ(id);
    }
}
