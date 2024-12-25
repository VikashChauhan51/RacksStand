namespace RacksStand.Application.Features.Categories.UpdateCategory;
internal class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        return new Result<Unit>(Unit.Value);
    }
}
