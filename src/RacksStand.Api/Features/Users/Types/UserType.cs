using RacksStand.Domain.Entities;
using HotChocolate.Types;

namespace RacksStand.Api.Features.Users.Types;


public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor.Field(u => u.FullName).Type<StringType>();
        descriptor.Field(u => u.UserName).Type<StringType>();
        descriptor.Field(u => u.Email).Type<StringType>();
        descriptor.Field(u => u.PasswordHash).Type<StringType>();
        descriptor.Field(u => u.PhoneNumber).Type<StringType>();
        descriptor.Field(u => u.PhoneNumberConfirmed).Type<BooleanType>();
        descriptor.Field(u => u.IsEmailConfirmed).Type<BooleanType>();
        descriptor.Field(u => u.LockoutEnd).Type<DateTimeType>();
        descriptor.Field(u => u.LockoutEnabled).Type<BooleanType>();
        descriptor.Field(u => u.AccessFailedCount).Type<IntType>();

        //descriptor.Field(u => u.UserPermissions)
        //    .Type<ListType<UserPermissionType>>();

        //descriptor.Field(u => u.Companies)
        //    .Type<ListType<CompanyUserType>>();

        //descriptor.Field(u => u.UserAccessTokens)
        //    .Type<ListType<UserAccessTokenType>>();

        //descriptor.Field(u => u.UserRefreshTokens)
        //    .Type<ListType<UserRefreshTokenType>>();
    }
}



