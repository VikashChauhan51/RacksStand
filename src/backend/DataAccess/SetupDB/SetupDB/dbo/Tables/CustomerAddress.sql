CREATE TABLE [dbo].[CustomerAddress]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
	[CustomerId] VARCHAR(32) NOT NULL,
    [Street] NVARCHAR(150) NOT NULL, 
    [Country] TINYINT NOT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [City] NVARCHAR(50) NOT NULL, 
    [Phone] VARCHAR(20) NULL, 
	[Email] VARCHAR(50) NULL, 
    [Fax] VARCHAR(20) NULL, 
    [ZipCode] VARCHAR(10) NOT NULL, 
    [Remark] NVARCHAR(150) NULL, 
    [Status] TINYINT NOT NULL
)
