using RacksStand.Api.Endpoints.Core;
using RacksStand.Models;
using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;

namespace RacksStand.Api.Endpoints.Features.Categories.Shared;

public class CategoryMappings : IMappingConfiguration
{
    public void Configure()
    {
        TypeAdapterConfig<CategoryRequest, CategoryModel>
           .NewConfig()
           .Map(dest => dest.Name, src => src.Name)
           .Map(dest => dest.Description, src => src.Description);

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
    }
}
