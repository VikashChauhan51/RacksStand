using Microsoft.EntityFrameworkCore;
using RacksStand.Api.Core.Domain.Entities;

namespace RacksStand.Api.Core.Infrastructure.Repositories;

public class UserRepository : Repository<User, string>, IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {

    }
}
