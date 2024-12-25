using RacksStand.Models;

namespace RacksStand.Application.Features.Categories.GetCategories;
public record GetCategoriesQueryResponse(PaginatedResult<CategoryModel> Categories);
