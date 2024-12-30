using RacksStand.Api.Core.Abstractions;
using RacksStand.Api.Features.Categories.Infrastructure.Data.Contexts;
using RacksStand.Api.Features.Categories.Repositories;
using RacksStand.Api.Features.Categories.Services;

namespace RacksStand.Api.Features.Categories.Infrastructure;

public class CategoryModule : IFeatureModule
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddScoped<CategoryContext>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICategoryService, CategoryService>();
        CategoryMappings.Configure();
    }
}
