namespace RacksStand.Api.Domain.Entities.Configurations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class UserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
    {
        builder.ToTable("user_tokens");
        builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
    }
}
