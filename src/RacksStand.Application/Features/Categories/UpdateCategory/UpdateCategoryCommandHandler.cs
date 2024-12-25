using MediatorForge.CQRS.Commands;
using Microsoft.Extensions.Logging;
using RacksStand.Abstraction.Services;
using RacksStand.Domain.Exceptions;

namespace RacksStand.Application.Features.Categories.UpdateCategory;
internal class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand, Result<Unit>>
{
    private readonly ICategoryService categoryService;
    private readonly ILogger<UpdateCategoryCommandHandler> logger;
    public UpdateCategoryCommandHandler(ICategoryService categoryService, ILogger<UpdateCategoryCommandHandler> logger)
    {
        this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<Unit>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await this.categoryService.GetByIdAsync(request.Id, cancellationToken);
        if (category == null)
        {
            this.logger.LogError("Category not found for ID:{Id}", request.Id);
            return Result<Unit>.Fail(new EntityNotFoundException($"Category not found for ID:{request.Id}"));
        }

        category.Name = request.Name;
        category.Description = request.Description;
        category.UpdatedAt = DateTime.UtcNow;
        // update entity
        await this.categoryService.UpdateAsync(category, cancellationToken);
        this.logger.LogDebug("The category has been successfully updated with ID: {Id}.", category.Id);
        return new Result<Unit>(Unit.Value);
    }
}
