using RacksStand.Api.Domain.Entities;

namespace RacksStand.Api.Domain;

public static class InitialDataSeeder
{
    public static void Seed(ModelBuilder builder)
    {
        var seedDate = new DateTimeOffset(2025, 8, 26, 0, 0, 0, TimeSpan.Zero);
        // ------------------ Roles ------------------
        var roles = new[] {
            new ApplicationRole { Id = "role-owner", Name = "Owner", NormalizedName = "OWNER", ConcurrencyStamp = "00000000-0000-0000-0000-000000000001", CreatedAt = seedDate },
            new ApplicationRole { Id = "role-admin", Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = "00000000-0000-0000-0000-000000000002", CreatedAt = seedDate },
            new ApplicationRole { Id = "role-systemadmin", Name = "SystemAdmin", NormalizedName = "SYSTEMADMIN", ConcurrencyStamp = "00000000-0000-0000-0000-000000000003", CreatedAt = seedDate },
            new ApplicationRole { Id = "role-employee", Name = "Employee", NormalizedName = "EMPLOYEE", ConcurrencyStamp = "00000000-0000-0000-0000-000000000005", CreatedAt = seedDate },
            new ApplicationRole { Id = "role-accountant", Name = "Accountant", NormalizedName = "ACCOUNTANT", ConcurrencyStamp = "00000000-0000-0000-0000-000000000006", CreatedAt = seedDate },
            new ApplicationRole { Id = "role-manager", Name = "Manager", NormalizedName = "MANAGER", ConcurrencyStamp = "00000000-0000-0000-0000-000000000007", CreatedAt = seedDate },
        };
        builder.Entity<ApplicationRole>().HasData(roles);
    }
}
