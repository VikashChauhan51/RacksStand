CREATE TABLE [dbo].[Racks]
(
	[Id] varchar(32) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[RoomId] VARCHAR(32) NOT NULL,
	[CompanyId] varchar(32) NOT NULL,
	[Description] NVARCHAR(250) NULL, 
	[Rows] int NOT NULL,
	[Columns] int NOT NULL,
	[BoxCapacity] int NOT NULL,
	[Index] INT,
    [Status] TINYINT NOT NULL, 
	[SecondaryStatus] TINYINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [UpdatedBy] VARCHAR(32) NULL
)
