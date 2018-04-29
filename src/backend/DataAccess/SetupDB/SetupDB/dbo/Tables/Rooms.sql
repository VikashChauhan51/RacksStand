CREATE TABLE [dbo].[Rooms]
(
	[Id] varchar(32) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[StoreId] VARCHAR(32) NOT NULL,
	[Description] NVARCHAR(250) NULL, 
	[CompanyId] varchar(32) NOT NULL,
	[Index] INT,
    [Status] TINYINT NOT NULL, 
	[SecondaryStatus] TINYINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [UpdatedBy] VARCHAR(32) NULL
)
