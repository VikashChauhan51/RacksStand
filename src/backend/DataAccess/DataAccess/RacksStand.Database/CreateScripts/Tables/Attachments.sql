CREATE TABLE [dbo].[Attachments]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(150) NULL, 
    [CompanyId] VARCHAR(32) NULL, 
    [FileType] TINYINT NULL, 
    [Extension] VARCHAR(5) NULL, 
    [Size] BIGINT NULL, 
    [Description] NVARCHAR(500) NULL, 
    [CreatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NULL, 
    [UpdatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(32) NULL, 
    [Status] TINYINT NULL
)
