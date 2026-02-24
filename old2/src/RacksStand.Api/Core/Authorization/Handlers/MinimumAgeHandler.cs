using Microsoft.AspNetCore.Authorization;
using RacksStand.Api.Core.Authorization.Requirements;

namespace RacksStand.Api.Core.Authorization.Handlers;

public class MinimumAgeHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
        if (!context.User.HasClaim(c => c.Type == "birthdate"))
        {
            return Task.CompletedTask;
        }

        var birthdateClaim = context.User.FindFirst(c => c.Type == "birthdate")!.Value;
        if (DateTime.TryParse(birthdateClaim, out var birthdate))
        {
            var age = DateTime.Today.Year - birthdate.Year;
            if (birthdate.Date > DateTime.Today.AddYears(-age)) age--;

            if (age >= requirement.MinimumAge)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }
}
