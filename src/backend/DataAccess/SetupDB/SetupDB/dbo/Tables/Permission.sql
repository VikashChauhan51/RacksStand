CREATE TABLE [dbo].[Permission]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(150) NULL, 
    [Status] TINYINT NOT NULL, 
    [ModuleId] INT NULL, 
    [CreatedOn] DATETIME NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NULL, 
    [UpdatedBy] VARCHAR(32) NULL
)
