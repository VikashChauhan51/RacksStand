namespace RacksStand.Api.Features.Categories.Models;

public record GetCategoriesRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
