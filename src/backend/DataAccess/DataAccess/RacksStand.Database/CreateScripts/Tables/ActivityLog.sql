CREATE TABLE [dbo].[ActivityLog]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [ActivityType] TINYINT NOT NULL, 
    [TargetObjectId] VARCHAR(32) NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [CreatedBy] VARCHAR(32) NULL, 
    [IPAddress] NVARCHAR(50) NULL, 
    [Latitude] FLOAT NULL, 
    [Longitude] FLOAT NULL, 
    [BrowserType] NVARCHAR(50) NULL, 
    [BrowserName] NVARCHAR(50) NULL, 
    [LogType] TINYINT NOT NULL
)
