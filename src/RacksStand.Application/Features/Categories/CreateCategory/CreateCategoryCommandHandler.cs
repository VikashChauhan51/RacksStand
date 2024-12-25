using MediatorForge.CQRS.Commands;
using Microsoft.Extensions.Logging;
using RacksStand.Abstraction.Services;
using RacksStand.Domain.Entities;
using ValueObjects = RacksStand.Domain.ValueObjects;

namespace RacksStand.Application.Features.Categories.CreateCategory;
internal class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, Result<Unit>>
{
    private readonly ICategoryService categoryService;
    private readonly ILogger<CreateCategoryCommandHandler> logger;
    public CreateCategoryCommandHandler(ICategoryService categoryService, ILogger<CreateCategoryCommandHandler> logger)
    {
        this.categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<Result<Unit>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Adding category.");
        var category = new Category
        {
            Id = ValueObjects.CategoryId.Of(Guid.NewGuid().ToString()),
            Name = request.Name,
            Description = request.Description,
            CreatedAt = DateTime.UtcNow,
        };
        await this.categoryService.AddAsync(category, cancellationToken);
        this.logger.LogDebug("The category has been successfully added with ID: {Id}.", category.Id);
        return new Result<Unit>(Unit.Value);
    }
}
