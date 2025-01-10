namespace RacksStand.Api.Features.Products;

public record UpdateProductRequest
(
     string Name,
     string? Description,
     string? Brand,
     string? Type,
     string? Tags,
     string? Metadata
);
