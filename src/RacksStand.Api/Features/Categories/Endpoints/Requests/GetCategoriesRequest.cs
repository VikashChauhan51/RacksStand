namespace RacksStand.Api.Features.Categories.Endpoints.Requests;

public record GetCategoriesRequest
(
    [FromQuery] int pageSize,
    [FromQuery] int pageNumber,
    [FromQuery] string? filter
);
