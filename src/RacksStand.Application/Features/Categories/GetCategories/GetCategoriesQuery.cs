using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Categories.GetCategories;
public record GetCategoriesQuery
(
    int pageSize,
    int pageNumber,
    string? filter
) : IQuery<Result<GetCategoriesQueryResponse>>;
