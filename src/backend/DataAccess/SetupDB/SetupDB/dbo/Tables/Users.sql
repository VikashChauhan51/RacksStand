CREATE TABLE [dbo].[Users]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(10) NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [MiddleName] NVARCHAR(50) NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [CompanyId] VARCHAR(32) NOT NULL, 
    [Status] TINYINT NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [Phone] NVARCHAR(20) NOT NULL, 
    [Password] NVARCHAR(MAX) NOT NULL, 
    [AttachmentId] VARCHAR(32) NULL, 
    [ParentId] VARCHAR(32) NULL, 
    [TimeZone] TINYINT NULL, 
    [Country] TINYINT NULL, 
    [Language] TINYINT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NULL, 
    [UpdatedBy] VARCHAR(32) NULL
)
