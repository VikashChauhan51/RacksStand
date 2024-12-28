using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Features.Shops.Domain.Entities;
using RacksStand.Api.Features.Shops.Domain.ValueObjects;
using RacksStand.Api.Features.Shops.Endpoints.Requests;
using RacksStand.Models;

namespace RacksStand.Api.Features.Shops.Shared;

public class ShopMappings : IMappingConfiguration
{
    public void Configure()
    {

        TypeAdapterConfig<ShopRequest, ShopModel>
         .NewConfig()
         .Map(dest => dest.Name, src => src.Name)
         .Map(dest => dest.Address, src => src.Address)
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
