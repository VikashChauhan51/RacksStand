CREATE TABLE [dbo].[Inventory]
(
	[Id] varchar(32) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[RackBoxId] VARCHAR(32) NULL,
	[CompanyId] varchar(32) NOT NULL,
	[CustomerId] varchar(32) NOT NULL,
	[Description] NVARCHAR(250) NULL,
	[AccessCode] varchar(32) NULL,
	[Index] INT,
	[CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [UpdatedBy] VARCHAR(32) NULL,
	[Status] TINYINT NOT NULL
)
