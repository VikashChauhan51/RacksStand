CREATE TABLE [dbo].[Tokens]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [UserId] VARCHAR(32) NOT NULL, 
    [TokenType] TINYINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [Status] TINYINT NOT NULL, 
    [UpdatedOn] DATETIME NULL
)
