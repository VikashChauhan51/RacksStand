using RacksStand.Domain.Entities;
using RacksStand.Domain.ValueObjects;
using RacksStand.Models;

namespace RacksStand.Api;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<Category, CategoryModel>
            .NewConfig()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description);

        TypeAdapterConfig<CategoryModel, Category>
            .NewConfig()
            .Map(dest => dest.Id, src => CategoryId.Of(src.Id))
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.Products, src => new List<Product>());
    }
}

