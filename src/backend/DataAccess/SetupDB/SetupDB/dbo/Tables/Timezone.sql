CREATE TABLE [dbo].[Timezone]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(250) NOT NULL, 
    [Code] VARCHAR(5) NOT NULL, 
    [OffSet] TINYINT NOT NULL, 
    [Status] TINYINT NOT NULL
)
