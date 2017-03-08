CREATE TABLE [dbo].[Plan]
(
	[Id] TINYINT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Price] BIGINT NULL, 
    [RickFreeddDays] TINYINT NULL, 
    [Contacts] BIGINT NULL, 
    [Orders] BIGINT NULL, 
    [Warehouses] INT NULL, 
    [CreateOn] DATETIME NULL, 
    [UpdateOn] DATETIME NULL, 
    [Status] TINYINT NULL, 
    [CustomeFields] INT NULL, 
    [Users] BIGINT NULL, 
    [Storage] BIGINT NULL, 
    [Calling] BIT NULL, 
    [MobileApp] BIT NULL
)
