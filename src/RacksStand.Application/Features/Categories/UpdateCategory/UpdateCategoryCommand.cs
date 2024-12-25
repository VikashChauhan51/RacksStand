using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Categories.UpdateCategory;
public record UpdateCategoryCommand
(
    string Id,
    string Name,
    string? Description
) : ICommand<Result<Unit>>;
