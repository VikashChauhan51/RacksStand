CREATE TABLE [dbo].[RoleGroups]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [RoleId] VARCHAR(32) NOT NULL, 
    [CompanyId] VARCHAR(32) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NULL, 
    [UpdatedBy] VARCHAR(32) NULL, 
    [Status] TINYINT NOT NULL
)
