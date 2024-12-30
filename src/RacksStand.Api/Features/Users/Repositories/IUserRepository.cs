using RacksStand.Api.Core.Domain.Entities;
using RacksStand.Api.Core.Infrastructure.Repositories;

namespace RacksStand.Api.Features.Users.Repositories;

public interface IUserRepository : IRepository<User, string>
{
}
