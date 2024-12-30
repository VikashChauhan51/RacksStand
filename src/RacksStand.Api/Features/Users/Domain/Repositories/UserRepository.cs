using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Users.Domain.Entities;

namespace RacksStand.Api.Features.Users.Domain.Repositories;

public class UserRepository : Repository<User, string>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {

    }
}
