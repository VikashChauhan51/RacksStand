using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using RacksStands.Framework.Modules.Bootstrap;
using RacksStands.Module.UserManagement.Repositories;
using RacksStands.Module.UserManagement.Endpoints;
using RacksStands.Framework.Modules.Bootstrap.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;

namespace RacksStands.Module.UserManagement;

public class UserManagementModule : IModule
{
    private readonly string _connectionString;

    public UserManagementModule(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UserManagementDbContext>(options =>
            options.UseSqlServer(_connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
    }

    public void MapEndpoints(IEndpointRouteBuilder routes)
    {
        routes.MapUserEndpoints();
    }
}
