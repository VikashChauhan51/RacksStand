using MediatorForge.CQRS.Queries;

namespace RacksStand.Application.Features.Categories.GetCategory;
internal class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, Result<GetCategoryQueryResponse>>
{
    public Task<Result<GetCategoryQueryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException(); 
    }
}
