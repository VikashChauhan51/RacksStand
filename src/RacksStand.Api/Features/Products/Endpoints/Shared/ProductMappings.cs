using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Domain.ValueObjects;
using RacksStand.Api.Core.Infrastructure;
using RacksStand.Models;

namespace RacksStand.Api.Features.Products.Endpoints.Shared;

public class ProductMappings : IMappingConfiguration
{
    public void Configure()
    {
        TypeAdapterConfig<ProductRequest, ProductModel>
           .NewConfig()
           .Map(dest => dest.Name, src => src.Name)
           .Map(dest => dest.Description, src => src.Description)
           .Map(dest => dest.SupplierId, src => src.SupplierId)
           .Map(dest => dest.Code, src => src.Code)
           .Map(dest => dest.Sku, src => src.Sku)
           .Map(dest => dest.PurchasePrice, src => src.PurchasePrice)
           .Map(dest => dest.SellingPrice, src => src.SellingPrice)
           .Map(dest => dest.Unit, src => src.Unit);


        TypeAdapterConfig<Product, ProductModel>
          .NewConfig()
          .Map(dest => dest.Id, src => src.Id.Value)
          .Map(dest => dest.Name, src => src.Name)
          .Map(dest => dest.Description, src => src.Description)
          .Map(dest => dest.SupplierId, src => src.SupplierId != null ? src.SupplierId.Value : null)
          .Map(dest => dest.Code, src => src.Code)
          .Map(dest => dest.Sku, src => src.Sku)
          .Map(dest => dest.PurchasePrice, src => src.PurchasePrice.Value)
          .Map(dest => dest.SellingPrice, src => src.SellingPrice.Value)
          .Map(dest => dest.Unit, src => src.Unit.Value);

        TypeAdapterConfig<ProductModel, Product>
            .NewConfig()
            .Map(dest => dest.Id, src => CategoryId.Of(src.Id))
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.SupplierId, src => src.SupplierId != null ? SupplierId.Of(src.SupplierId) : null)
            .Map(dest => dest.Code, src => src.Code)
            .Map(dest => dest.Sku, src => src.Sku)
            .Map(dest => dest.PurchasePrice, src => PurchasePrice.Of(src.PurchasePrice))
            .Map(dest => dest.SellingPrice, src => SellingPrice.Of(src.SellingPrice))
            .Map(dest => dest.Unit, src => Unit.Of(src.Unit));
    }
}
