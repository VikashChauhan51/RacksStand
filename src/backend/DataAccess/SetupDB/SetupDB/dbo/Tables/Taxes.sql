CREATE TABLE [dbo].[Taxes]
(
	[Id] varchar(32) NOT NULL PRIMARY KEY,
	[CompanyId] varchar(32) NOT NULL,
	[Code] nvarchar(10) NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Rate] float NOT NULL,
	[IsCompound] bit NOT NULL,
	[IsActive] bit NOT NULL
)
