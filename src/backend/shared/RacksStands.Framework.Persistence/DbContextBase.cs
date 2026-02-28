using Microsoft.EntityFrameworkCore;

namespace RacksStands.Framework.Persistence;

public abstract class DbContextBase : DbContext
{
    protected DbContextBase(DbContextOptions options) : base(options) { }
}
