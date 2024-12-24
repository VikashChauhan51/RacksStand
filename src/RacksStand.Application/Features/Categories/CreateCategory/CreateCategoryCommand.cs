using MediatorForge.CQRS.Commands;
using MediatR;
using ResultifyCore;

namespace RacksStand.Application.Features.Categories.CreateCategory;
public record CreateCategoryCommand
(
    string Name
) : ICommand<Result<Unit>>;
