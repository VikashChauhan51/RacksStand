ALTER TABLE [dbo].[CustomerAddress]
	ADD CONSTRAINT [FK_Customer_Id_CustomerAddress_CustomerId]
	FOREIGN KEY (CustomerId)
	REFERENCES [dbo].[Customers](Id)
