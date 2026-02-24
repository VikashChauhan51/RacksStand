using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using RacksStands.Framework.Modules.Base;
using RacksStands.Module.UserManagement.Repositories;
using RacksStands.Module.UserManagement.Endpoints;

namespace RacksStands.Module.UserManagement;

public class UserManagementModule : IModule
{
    private readonly string _connectionString;

    public UserManagementModule(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<UserManagementDbContext>(options =>
            options.UseSqlServer(_connectionString));

        services.AddScoped<IUserRepository, UserRepository>();
    }

    public void ConfigureApplication(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserManagementDbContext>();
        dbContext.Database.Migrate();

        app.MapUserEndpoints();
    }
}
