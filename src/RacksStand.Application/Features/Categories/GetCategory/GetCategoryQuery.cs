using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Categories.GetCategory;
public record GetCategoryQuery(string Id):IQuery<Result<GetCategoryQueryResponse>>;
