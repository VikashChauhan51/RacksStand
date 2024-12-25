using Microsoft.EntityFrameworkCore;
using RacksStand.Abstraction.Repositories;
using RacksStand.Abstraction.Services;
using RacksStand.Domain.Entities;

namespace RacksStand.Infrastructure.Services;
public class CategoryService : Service<Category, string>, ICategoryService
{
    public CategoryService(IRepository<Category> repository, IUnitOfWork unitOfWork)
        : base(repository, unitOfWork)
    {
    }

}

