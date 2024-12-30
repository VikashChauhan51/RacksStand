using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Infrastructure;
using RacksStand.Api.Features.Suppliers.Models;
using RacksStand.Models;

namespace RacksStand.Api.Features.Suppliers;

public class SupplierMappings : IMappingConfiguration
{
    public void Configure()
    {
        TypeAdapterConfig<SupplierRequest, SupplierModel>
          .NewConfig()
          .Map(dest => dest.Name, src => src.Name)
          .Map(dest => dest.Code, src => src.Code);

        TypeAdapterConfig<Supplier, SupplierModel>
          .NewConfig()
          .Map(dest => dest.Id, src => src.Id.Value)
          .Map(dest => dest.Name, src => src.Name)
          .Map(dest => dest.ShopId, src => src.ShopId.Value)
          .Map(dest => dest.Code, src => src.Code);

        TypeAdapterConfig<SupplierModel, Supplier>
            .NewConfig()
            .Map(dest => dest.Id, src => CategoryId.Of(src.Id))
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.ShopId, src => ShopId.Of(src.ShopId))
            .Map(dest => dest.Code, src => src.Code);
    }
}
