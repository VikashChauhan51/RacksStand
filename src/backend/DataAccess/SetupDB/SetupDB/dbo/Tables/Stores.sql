CREATE TABLE [dbo].[Stores]
(
	[Id] varchar(32) NOT NULL PRIMARY KEY,
	[Name] NVARCHAR(100) NOT NULL,
	[CompanyId] varchar(32) NOT NULL,
    [Street] NVARCHAR(150) NOT NULL, 
    [Country] TINYINT NOT NULL, 
    [State] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [Phone] VARCHAR(20) NULL, 
    [Fax] VARCHAR(20) NULL, 
	[Email] VARCHAR(50) NULL, 
    [ZipCode] VARCHAR(10) NULL, 
    [Description] NVARCHAR(250) NULL, 
	[Index] INT,
    [Status] TINYINT NOT NULL, 
	[SecondaryStatus] TINYINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [UpdatedBy] VARCHAR(32) NULL
)
