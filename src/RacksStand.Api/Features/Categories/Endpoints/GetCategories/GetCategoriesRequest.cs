namespace RacksStand.Api.Features.Categories.Endpoints.GetCategories;

public record GetCategoriesRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
