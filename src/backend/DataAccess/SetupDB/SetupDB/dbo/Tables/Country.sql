CREATE TABLE [dbo].[Country]
(
	[Id] TINYINT NOT NULL PRIMARY KEY, 
    [Name] NCHAR(10) NOT NULL, 
    [Status] TINYINT NULL, 
    [Code] VARCHAR(5) NULL
)
