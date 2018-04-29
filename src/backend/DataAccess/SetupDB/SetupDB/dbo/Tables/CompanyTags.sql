CREATE TABLE [dbo].[CompanyTags]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [CompanyId] VARCHAR(32) NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NULL, 
    [UpdatedBy] VARCHAR(32) NULL, 
    [Status] TINYINT NOT NULL
)
