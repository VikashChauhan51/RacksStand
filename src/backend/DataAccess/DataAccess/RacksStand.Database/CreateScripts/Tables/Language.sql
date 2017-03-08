CREATE TABLE [dbo].[Language]
(
	[Id] TINYINT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NULL, 
    [Code] VARCHAR(5) NULL, 
    [FlagId] VARCHAR(32) NULL, 
    [Status] TINYINT NULL
)
