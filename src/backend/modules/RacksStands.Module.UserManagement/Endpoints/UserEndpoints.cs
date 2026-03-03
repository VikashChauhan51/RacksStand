using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using RacksStands.Module.UserManagement.Entities;
using RacksStands.Module.UserManagement.Repositories;
using RacksStands.Module.UserManagement.Requests;

namespace RacksStands.Module.UserManagement.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/users", async (IUserRepository repo) =>
        {
            var users = await repo.GetAllAsync();
            return Results.Ok(users);
        });

        routes.MapGet("/api/users/{id}", async (Guid id, IUserRepository repo) =>
        {
            var user = await repo.GetByIdAsync(id);
            return user is not null ? Results.Ok(user) : Results.NotFound();
        });

        routes.MapPost("/api/users", async (CreateUserRequest request, IUserRepository repo) =>
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email,
                PasswordHash = request.Password,
                CreatedAt = DateTime.UtcNow
            };
            await repo.AddAsync(user);
            return Results.Created($"/api/users/{user.Id}", user);
        });

        // Add more endpoints
    }
}
