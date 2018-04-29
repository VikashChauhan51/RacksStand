CREATE TABLE [dbo].[LoginHistory]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [UserId] VARCHAR(32) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [Browser] VARCHAR(50) NULL, 
    [Platform] VARCHAR(50) NULL, 
    [Version] VARCHAR(20) NULL, 
    [IsMobileDevice] BIT NULL, 
    [HostName] VARCHAR(80) NULL, 
    [HostAddress] VARCHAR(20) NULL, 
    [URI] VARCHAR(120) NULL
)
