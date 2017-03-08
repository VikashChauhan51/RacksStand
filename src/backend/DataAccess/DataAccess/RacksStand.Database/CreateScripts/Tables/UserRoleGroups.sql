CREATE TABLE [dbo].[UserRoleGroups]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [UserId] VARCHAR(32) NOT NULL, 
    [RoleGroupId] VARCHAR(32) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] DATETIME NULL, 
    [UpdatedBy] DATETIME NULL, 
    [Status] TINYINT NOT NULL
)
