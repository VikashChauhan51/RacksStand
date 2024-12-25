using Mapster;
using MediatorForge.CQRS.Queries;
using Microsoft.Extensions.Logging;
using RacksStand.Abstraction.Services;
using RacksStand.Domain.Exceptions;
using RacksStand.Models;

namespace RacksStand.Application.Features.Categories.GetCategory;
internal class GetCategoryQueryHandler : IQueryHandler<GetCategoryQuery, Result<GetCategoryQueryResponse>>
{
    private readonly ICategoryService categoryService;
    private readonly ILogger<GetCategoryQueryHandler> logger;
    public GetCategoryQueryHandler(ICategoryService categoryService, ILogger<GetCategoryQueryHandler> logger)
    {
        this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<GetCategoryQueryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        this.logger.LogDebug("Fatching category for ID:{Id}", request.Id);
        var category = await this.categoryService.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
        {
            this.logger.LogError("Category not found for ID:{Id}", request.Id);
            return
                Result<GetCategoryQueryResponse>
                .Fail(new EntityNotFoundException($"Category not found for ID:{request.Id}"));
        }
        return new Result<GetCategoryQueryResponse>
        (
            new GetCategoryQueryResponse(category.Adapt<CategoryModel>())
        );
    }
}
