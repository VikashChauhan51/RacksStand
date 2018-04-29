CREATE TABLE [dbo].[EmailServers]
(
	[Id] TINYINT NOT NULL PRIMARY KEY, 
    [UserName] VARCHAR(100) NOT NULL, 
    [Password] VARCHAR(100) NOT NULL, 
    [Host] VARCHAR(100) NOT NULL, 
    [Port] INT NOT NULL, 
    [EnableSsl] BIT NOT NULL, 
    [IsBodyHtml] BIT NOT NULL, 
    [Status] TINYINT NOT NULL
)
