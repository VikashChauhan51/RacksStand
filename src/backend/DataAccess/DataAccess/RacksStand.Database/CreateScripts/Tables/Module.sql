CREATE TABLE [dbo].[Module]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(150) NULL, 
    [Status] TINYINT NOT NULL
)
