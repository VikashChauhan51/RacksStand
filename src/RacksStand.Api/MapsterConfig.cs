using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;
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
            .Map(dest => dest.ShopId, src => src.ShopId.Value)
            .Map(dest => dest.Description, src => src.Description);

        TypeAdapterConfig<CategoryModel, Category>
            .NewConfig()
            .Map(dest => dest.Id, src => CategoryId.Of(src.Id))
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.ShopId, src => ShopId.Of(src.ShopId))
            .Map(dest => dest.Description, src => src.Description);

        TypeAdapterConfig<Shop, ShopModel>
           .NewConfig()
           .Map(dest => dest.Id, src => src.Id.Value)
           .Map(dest => dest.Name, src => src.Name)
           .Map(dest => dest.Address, src => src.Address)
           .Map(dest => dest.OwnerId, src => src.OwnerId)
           .Map(dest => dest.Description, src => src.Description);

        TypeAdapterConfig<ShopModel, Shop>
            .NewConfig()
            .Map(dest => dest.Id, src => ShopId.Of(src.Id))
            .Map(dest => dest.Name, src => src.Name)
           .Map(dest => dest.Address, src => src.Address)
           .Map(dest => dest.OwnerId, src => src.OwnerId)
           .Map(dest => dest.Description, src => src.Description);
    }
}

