using RacksStand.Persistence.Entities;
using RacksStand.Persistence.ValueObjects;
using RacksStand.Models;
using RacksStand.Api.Endpoints.Features.Categories.Shared;
using RacksStand.Api.Endpoints.Features.Shops.Shared;
using RacksStand.Api.Endpoints.Features.Suppliers.Shared;
using RacksStand.Api.Endpoints.Features.Products.Shared;

namespace RacksStand.Api;

public static class MapsterConfig
{
    public static void RegisterMappings()
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

