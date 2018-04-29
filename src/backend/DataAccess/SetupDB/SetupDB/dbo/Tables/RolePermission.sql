CREATE TABLE [dbo].[RolePermission]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [RoleId] VARCHAR(32) NULL, 
    [Permission] INT NULL, 
    [CreatedOn] DATETIME NULL, 
    [CompanyId] VARCHAR(32) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NULL, 
    [UpdatedBy] VARCHAR(32) NULL, 
    [Status] TINYINT NULL
)
