using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using RacksStands.Module.UserManagement.Repositories;
using RacksStands.Module.UserManagement.Endpoints;

namespace RacksStands.Module.UserManagement;

public static class UserManagementModuleExtensions
{
    public static IServiceCollection AddUserManagementModule(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<UserManagementDbContext>(options =>
            options.UseSqlServer(connectionString)); // Assuming SQL Server

        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }

    public static void UseUserManagementModule(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<UserManagementDbContext>();
        dbContext.Database.Migrate(); // Apply migrations

        app.MapUserEndpoints();
    }
}
