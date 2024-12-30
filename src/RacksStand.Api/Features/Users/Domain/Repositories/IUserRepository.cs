using RacksStand.Api.Core.Repositories;
using RacksStand.Api.Features.Users.Domain.Entities;

namespace RacksStand.Api.Features.Users.Domain.Repositories;

public interface IUserRepository : IRepository<User, string>
{
}
