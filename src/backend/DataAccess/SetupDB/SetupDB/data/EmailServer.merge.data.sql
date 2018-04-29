-- Data for email server.
		   MERGE INTO EmailServers AS Target
		   USING ( VALUES
		   ( 1,'codeinvision@outlook.com','nahuahc@12345','smtp-mail.outlook.com',587,1,1,1)
		   )
		   AS Source(Id,UserName,Password,Host,Port,EnableSsl,IsBodyHtml,Status)
		   ON Target.Id=Source.Id
	-- insert new row
	WHEN NOT MATCHED BY Target THEN

	 INSERT (Id,UserName,Password,Host,Port,EnableSsl,IsBodyHtml,Status)
	 VALUES (Source.Id,Source.UserName,Source.Password,Source.Host,Source.Port,Source.EnableSsl,Source.IsBodyHtml,Source.Status);