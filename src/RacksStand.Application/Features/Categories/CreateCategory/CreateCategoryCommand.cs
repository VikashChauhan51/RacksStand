using MediatorForge.CQRS.Commands;

namespace RacksStand.Application.Features.Categories.CreateCategory;
public record CreateCategoryCommand
(
    string Name,
    string? Description
) : ICommand<Result<Unit>>;
