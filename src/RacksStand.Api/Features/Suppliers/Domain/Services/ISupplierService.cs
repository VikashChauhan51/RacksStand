using RacksStand.Models;

namespace RacksStand.Api.Features.Suppliers.Domain.Services;

public interface ISupplierService
{
    Task<Result<string>> AddSupplierAsync(SupplierModel model, CancellationToken cancellationToken = default);
    Task<Result<bool>> CheckIfSupplierExistsAsync(string name, string shopId, CancellationToken cancellationToken = default);
    Task<Result<string>> DeleteSupplierAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<SupplierModel>> GetSupplierAsync(string id, CancellationToken cancellationToken = default);
    Task<Result<PaginatedResult<SupplierModel>>> GetSuppliersAsync(string shopId, int pageSize, int pageNumber, string? filter, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateSupplierAsync(string id, SupplierModel model, CancellationToken cancellationToken = default);
}
