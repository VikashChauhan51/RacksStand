using FluentValidation;
using MediatR;
using ResultifyCore;

namespace RacksStand.Application.Features.Categories.CreateCategory;
internal class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result<Unit>>
{
    public CreateCategoryCommandHandler(IValidator<CreateCategoryCommand> validator)
    {
    }
    public async Task<Result<Unit>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return new Result<Unit>(Unit.Value);
    }
}
