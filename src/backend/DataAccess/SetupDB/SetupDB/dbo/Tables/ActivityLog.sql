CREATE TABLE [dbo].[ActivityLog]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [ActivityType] TINYINT NOT NULL, 
    [TargetObjectId] VARCHAR(32) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [HostAddress] VARCHAR(20) NULL, 
    [HostName] VARCHAR(80) NULL, 
    [IsMobileDevice] BIT NULL, 
    [Browser] NVARCHAR(50) NULL, 
    [Platform] NVARCHAR(50) NULL, 
    [Version] VARCHAR(20) NULL, 
    [URI] VARCHAR(120) NULL, 
    [Message] VARCHAR(50) NOT NULL
)
