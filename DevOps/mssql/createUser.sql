USE [master]
GO
CREATE LOGIN [parcadmin] WITH PASSWORD=N'!@pside2022!', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
GO
ALTER SERVER ROLE [dbcreator] ADD MEMBER [parcadmin]
GO
USE [parcIndus]
GO
CREATE USER [parcadmin] FOR LOGIN [parcadmin]
GO
USE [parcIndus]
GO
ALTER ROLE [db_owner] ADD MEMBER [parcadmin]
GO