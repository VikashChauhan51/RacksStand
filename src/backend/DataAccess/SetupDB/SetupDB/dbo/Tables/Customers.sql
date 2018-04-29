CREATE TABLE [dbo].[Customers]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(8) NULL, 
    [FirstName] VARCHAR(50) NOT NULL, 
    [MiddleName] VARCHAR(50) NULL, 
    [LastName] VARCHAR(50) NOT NULL, 
    [Company] VARCHAR(50) NULL, 
    [Email] VARCHAR(50) NULL, 
    [Bio] VARCHAR(250) NULL, 
    [Phone] VARCHAR(20) NULL, 
    [Mobile] VARCHAR(20) NULL, 
    [Fax] VARCHAR(20) NULL, 
    [PanNo] VARCHAR(20) NULL,  
    [Website] VARCHAR(100) NULL,  
    [Status] TINYINT NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [UpdatedOn] DATETIME NULL, 
    [UpdatedBy] VARCHAR(32) NULL, 
    [CompanyId] VARCHAR(32) NOT NULL
)
