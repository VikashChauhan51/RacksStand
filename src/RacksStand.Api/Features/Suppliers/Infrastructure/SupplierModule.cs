using RacksStand.Api.Core.Abstractions;
using RacksStand.Api.Features.Suppliers.Repositories;
using RacksStand.Api.Features.Suppliers.Services;

namespace RacksStand.Api.Features.Suppliers.Infrastructure;

public class SupplierModule : IFeatureModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ISupplierRepository, SupplierRepository>();
        services.AddScoped<ISupplierService, SupplierService>();
        SupplierMappings.Configure();
    }
}
