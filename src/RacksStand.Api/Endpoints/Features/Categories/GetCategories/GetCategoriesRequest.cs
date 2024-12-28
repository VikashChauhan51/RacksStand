namespace RacksStand.Api.Endpoints.Features.Categories.GetCategories;

public record GetSuppliersRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
