CREATE TABLE [dbo].[Attachments]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(150) NOT NULL, 
    [CompanyId] VARCHAR(32) NOT NULL, 
    [FileType] TINYINT NOT NULL, 
    [Extension] VARCHAR(5) NOT NULL, 
    [Size] BIGINT NOT NULL, 
    [Description] NVARCHAR(500) NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(32) NULL, 
    [Status] TINYINT NOT NULL
)
