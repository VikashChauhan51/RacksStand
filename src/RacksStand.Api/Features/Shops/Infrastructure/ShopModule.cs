using RacksStand.Api.Core.Abstractions;
using RacksStand.Api.Features.Shops.Infrastructure.Data.Contexts;
using RacksStand.Api.Features.Shops.Repositories;
using RacksStand.Api.Features.Shops.Services;

namespace RacksStand.Api.Features.Shops.Infrastructure;

public class ShopModule : IFeatureModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<ShopContext>();
        services.AddScoped<IShopRepository, ShopRepository>();
        services.AddScoped<IShopService, ShopService>();
        ShopMappings.Configure();
    }
}
