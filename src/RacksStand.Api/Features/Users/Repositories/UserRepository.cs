using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Infrastructure.Data.Contexts;
using RacksStand.Api.Core.Infrastructure.Repositories;

namespace RacksStand.Api.Features.Users.Repositories;

public class UserRepository : Repository<User, string>, IUserRepository
{
    public UserRepository(RacksStandContext context) : base(context)
    {

    }
}
