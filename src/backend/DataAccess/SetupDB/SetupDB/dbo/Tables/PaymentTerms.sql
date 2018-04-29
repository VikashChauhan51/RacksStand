CREATE TABLE [dbo].[PaymentTerms]
(
    [Id]  varchar(32) NOT NULL PRIMARY KEY,
	[CompanyId] varchar(32) NOT NULL,
	[Code] nvarchar(10) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[NumberOfDays] int NOT NULL
)
