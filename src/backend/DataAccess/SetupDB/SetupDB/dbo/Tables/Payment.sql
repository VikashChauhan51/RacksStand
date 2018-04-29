CREATE TABLE [dbo].[Payment]
(
	[Id] VARCHAR(32) NOT NULL PRIMARY KEY, 
    [CompanyId] VARCHAR(32) NOT NULL, 
    [CreatedBy] VARCHAR(32) NOT NULL, 
    [CreatedOn] DATETIME NOT NULL, 
    [Status] TINYINT NOT NULL, 
    [PayAmt] DECIMAL(10, 2) NOT NULL
)
