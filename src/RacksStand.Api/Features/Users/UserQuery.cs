using RacksStand.Domain.Entities;
using RacksStand.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Users;

public class UserQuery
{
    public IQueryable<User> GetUsers([Service] RacksStandContext dbContext)
    {
        return dbContext.Users;
    }
}

