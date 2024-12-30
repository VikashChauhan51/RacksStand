using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Infrastructure.Repositories;
using RacksStand.Api.Features.Users.Infrastructure.Data.Contexts;

namespace RacksStand.Api.Features.Users.Repositories;

public class UserRepository : Repository<User, string>, IUserRepository
{
    public UserRepository(UserContext context) : base(context)
    {

    }
}
