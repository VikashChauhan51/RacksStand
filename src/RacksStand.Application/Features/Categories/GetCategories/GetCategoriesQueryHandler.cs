using Mapster;
using MediatorForge.CQRS.Queries;
using Microsoft.Extensions.Logging;
using RacksStand.Abstraction.Services;
using RacksStand.Models;

namespace RacksStand.Application.Features.Categories.GetCategories;
internal class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, Result<GetCategoriesQueryResponse>>
{
    private readonly ICategoryService categoryService;
    private readonly ILogger<GetCategoriesQueryHandler> logger;
    public GetCategoriesQueryHandler(ICategoryService categoryService, ILogger<GetCategoriesQueryHandler> logger)
    {
        this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<GetCategoriesQueryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("Fatching categories for PageNumber:{PageNumber} and PageSize:{PageSize}", request.pageNumber, request.pageSize);
        var response = await this.categoryService.GetPaginatedAsync(request.pageNumber, request.pageSize, true, null, null, cancellationToken);

        if (response == null)
        {
            return Result<GetCategoriesQueryResponse>.Fail(new Exception("Categories not found."));
        }
        return new Result<GetCategoriesQueryResponse>
        (
            new GetCategoriesQueryResponse
            (
                new PaginatedResult<CategoryModel>
                {
                    Items = response.Items.Select(x => x.Adapt<CategoryModel>()),
                    TotalItems = response.TotalItems,
                    PageNumber = response.PageNumber,
                    PageSize = response.PageSize
                }
            )
        );
    }
}
