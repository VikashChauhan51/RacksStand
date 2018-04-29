CREATE TABLE [dbo].[Currencies]
(
	[Id]  varchar(32) NOT NULL PRIMARY KEY,
	[CompanyId] varchar(32) NOT NULL,
	[Code] nvarchar(10) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[ExchangeRate] float NOT NULL,
	[Symbol] nvarchar(3),
	[IsActive] bit NOT NULL
)
