CREATE TABLE [dbo].[UrlLinks]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [Value] VARCHAR(100) NULL,
	 [Status] TINYINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(32) NULL
)
