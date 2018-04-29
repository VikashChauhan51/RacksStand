CREATE TABLE [dbo].[CompanyRoles]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(150) NULL, 
    [CompanyId] VARCHAR(32) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NULL, 
    [UpdatedBy] VARCHAR(32) NULL, 
    [Status] TINYINT NOT NULL
)
