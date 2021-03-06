﻿CREATE TABLE [dbo].[Address]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Street] NVARCHAR(150) NOT NULL, 
    [Country] TINYINT NOT NULL, 
    [State] NVARCHAR(50) NULL, 
    [City] NVARCHAR(50) NULL, 
    [Phone] VARCHAR(20) NULL, 
    [Fax] VARCHAR(20) NULL, 
    [ZipCode] VARCHAR(10) NULL, 
    [Remark] NVARCHAR(150) NULL, 
    [Status] TINYINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [UpdatedBy] VARCHAR(32) NULL
)
