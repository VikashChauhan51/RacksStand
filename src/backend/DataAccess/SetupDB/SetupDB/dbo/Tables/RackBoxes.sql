CREATE TABLE [dbo].[RackBoxes]
(
	[Id] varchar(32) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[RackId] VARCHAR(32) NOT NULL,
	[CompanyId] varchar(32) NOT NULL,
	[Description] NVARCHAR(250) NULL,
	[Row] int NOT NULL,
	[Column] int NOT NULL,
	[Index] INT,
	[SecondaryStatus] TINYINT NOT NULL, 
	[CurrentSize] int
)
