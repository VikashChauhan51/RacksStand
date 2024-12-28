namespace RacksStand.Api.Endpoints.Features.Categories.GetCategories;

public record GetCategoriesRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
