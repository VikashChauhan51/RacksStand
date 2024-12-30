using RacksStand.Api.Core.Abstractions;
using RacksStand.Api.Features.Products.Infrastructure.Data.Contexts;
using RacksStand.Api.Features.Products.Repositories;
using RacksStand.Api.Features.Products.Services;

namespace RacksStand.Api.Features.Products.Infrastructure;

public class ProductModule : IFeatureModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ProductContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductService, ProductService>();
        ProductMappings.Configure();
    }
}
