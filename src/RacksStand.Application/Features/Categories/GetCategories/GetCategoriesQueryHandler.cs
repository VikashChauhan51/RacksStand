using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Categories.GetCategories;
internal class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, Result<GetCategoriesQueryResponse>>
{
    public Task<Result<GetCategoriesQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
