using RacksStand.Api.Core.Abstractions;
using RacksStand.Api.Features.Users.Repositories;
using RacksStand.Api.Features.Users.Services;

namespace RacksStand.Api.Features.Users.Infrastructure;

public class UserModule : IFeatureModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<IUserPlanRepository, UserPlanRepository>();
        services.AddScoped<IUserShopRepository, UserShopRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
    }
}
