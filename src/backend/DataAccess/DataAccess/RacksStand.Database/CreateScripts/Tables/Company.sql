CREATE TABLE [dbo].[Company]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [AppName] NVARCHAR(50) NOT NULL, 
    [Website] VARCHAR(150) NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(32) NULL, 
    [Status] TINYINT NOT NULL, 
    [AttachmentId] VARCHAR(32) NULL, 
    [Currency] INT NULL, 
    [Industry] INT NULL, 
    [PrimaryContact] VARCHAR(32) NOT NULL, 
    [PrimaryAddress] VARCHAR(32) NULL, 
    [TaxId] VARCHAR(32) NULL, 
    [CompanyId] NVARCHAR(50) NULL
)
