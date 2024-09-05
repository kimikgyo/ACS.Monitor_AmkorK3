-----------------------------------------------
-- [monitor] 계정 생성 및 권한 설정
-----------------------------------------------

USE [master]
CREATE LOGIN [monitor] WITH PASSWORD=N'monitor1111', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF

USE [AmkorK5_T3F_M3F]
CREATE USER [monitor] FOR LOGIN [monitor]
ALTER ROLE [db_datareader] ADD MEMBER [monitor]
ALTER ROLE [db_datawriter] ADD MEMBER [monitor]

USE [AmkorK5_User_Data]
CREATE USER [monitor] FOR LOGIN [monitor]
ALTER ROLE [db_datareader] ADD MEMBER [monitor]
ALTER ROLE [db_datawriter] ADD MEMBER [monitor]





--USE [master]
--CREATE LOGIN [monitor] WITH PASSWORD=N'monitor1111', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF


--USE [AmkorK5_T3F_M3F]
--CREATE USER [monitor] FOR LOGIN [monitor]
--GRANT SELECT TO [monitor]
----GRANT INSERT TO [monitor]
----GRANT DELETE TO [monitor]
----GRANT UPDATE TO [monitor]
--REVOKE INSERT FROM [monitor]
--REVOKE DELETE FROM [monitor]
--REVOKE UPDATE FROM [monitor]


--USE [AmkorK5_User_Data]
--CREATE USER [monitor] FOR LOGIN [monitor]
--GRANT SELECT TO [monitor]
--GRANT INSERT TO [monitor]
--GRANT DELETE TO [monitor]
----GRANT UPDATE TO [monitor]





-------------------------------------------------
---- [monitor] 계정 삭제
-------------------------------------------------
----use [AmkorK5_T3F_M3F]
----drop user if exists [monitor]
----use [AmkorK5_User_Data]
----drop user if exists [monitor]
----USE [master]
----drop login monitor
