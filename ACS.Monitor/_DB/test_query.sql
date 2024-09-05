use [HYUNDAI_TRANSYS_Gyeongju]

--UPDATE [dbo].[MissionConfigs] SET [ACSMissionGroup] = 'Hook_Rail' WHERE CallButtonName like 'MC1%'
--UPDATE [dbo].[MissionConfigs] SET [ACSMissionGroup] = 'Lift_Recliner' WHERE CallButtonName like 'MC3%'

--UPDATE [dbo].[MissionConfigs] SET [JobMissionName1] = 'None'
--UPDATE [dbo].[MissionConfigs] SET [JobMissionName2] = 'None'
--UPDATE [dbo].[MissionConfigs] SET [JobMissionName3] = NULL
--UPDATE [dbo].[MissionConfigs] SET [JobMissionName4] = NULL
--UPDATE [dbo].[MissionConfigs] SET [JobMissionName5] = NULL


SELECT * FROM [dbo].[PartModels] order by line_cd, post_cd		-- part
SELECT * FROM [dbo].[CallButtons] --WHERE ButtonName LIKE 'MC%' ORDER BY ButtonIndex	-- Caller status info
SELECT * FROM [dbo].[MissionConfigs] WHERE UseFlag=1 order by	 CallButtonIndex		-- caller config		====> job config

--UPDATE [dbo].[Robots] SET [ACSRobotActive] = 0

--ALTER TABLE [dbo].[Jobs] ADD PopCallTime datetime;  --컬럼추가

--UPDATE [dbo].[Missions] SET [RobotName] = 'MiR_204603262' WHERE Id = 2900
--UPDATE [dbo].[Missions] SET [MissionState] = 'Executing' WHERE Id = 2900

--delete from robots
--delete from jobs
--delete from missions
--delete from wmsmodels

--delete from Jobs where id=1141
--delete from Missions where id=3656

SELECT * FROM [dbo].[Robots]									-- robot
SELECT * FROM [dbo].[Jobs]										-- job
SELECT * FROM [dbo].[Missions]									-- mission

SELECT * FROM [dbo].[WMSModels] 
--SELECT * FROM [dbo].[WMSModels] where WMS_IF_FLAG='N' AND LINE_CD LIKE 'MC3%%' order by line_cd,post_cd
--SELECT * FROM [dbo].[WMSModels] where                     LINE_CD LIKE 'MC3%%' order by line_cd,post_cd


SELECT * FROM [dbo].[POPSERVER_1] WHERE ACS_IF_FLAG='N'
SELECT * FROM [dbo].[POPSERVER_2] WHERE ACS_IF_FLAG='N'
SELECT * FROM [dbo].[POPSERVER_3] WHERE ACS_IF_FLAG='N'
SELECT * FROM [dbo].[POPSERVER_4] WHERE ACS_IF_FLAG='N'


SELECT * FROM [dbo].[POPSERVER_1] WHERE ACS_IF_FLAG='N'
SELECT * FROM [dbo].[POPSERVER_1] WHERE CREATE_DT <= GETDATE()

SELECT CONVERT(NVARCHAR(8),DATEADD(DAY,-30,GETDATE()),112)AS '30일전'
SELECT DATEADD(DAY,-30,GETDATE()), day(getdate())
Select GETDATE(), Convert(varchar(23),Getdate(),21), Convert(varchar(10),Getdate(),23), Convert(varchar(8),Getdate(),112)  

SELECT DATEADD(DAY, -10, '2021-07-12') AS [10일전] 
SELECT DATEADD(DAY, 10, '2021-07-12') AS [10일후]

SELECT DATEADD(DAY, -7, GETDATE()) AS [7일전] 
SELECT Convert(varchar(14),DATEADD(DAY, -7, GETDATE()),23)  AS [7일전날짜] 
--SELECT * FROM [dbo].[POPSERVER_1] WHERE CREATE_DT < '2022-09-20 10:00:00.000'
SELECT * FROM [dbo].[POPSERVER_1] WHERE CREATE_DT < Convert(varchar(14),DATEADD(DAY, -7, GETDATE()),23)
SELECT * FROM [dbo].[POPSERVER_1] 


--delete from POPSERVER_1 where line_cd='mc310_1'
--delete from POPSERVER_1
--delete from POPSERVER_2
--delete from POPSERVER_3
--delete from POPSERVER_4

--insert into POPSERVER_1 VALUES ('MC110', 1, 'N', 0,  'N', NULL, 'N', GETDATE()) 
--insert into POPSERVER_1 VALUES ('MC110', 2, 'N', 0,  'Y', NULL, 'N', GETDATE()) 
--insert into POPSERVER_1 VALUES ('MC110', 3, 'N', 0,  'N', NULL, 'N', GETDATE()) 
--insert into POPSERVER_1 VALUES ('MC110', 4, 'N', 0,  'Y', NULL, 'N', GETDATE()) 
--insert into POPSERVER_4 VALUES ('MC351', 1, 'N', 0,  'N', NULL, 'N', GETDATE()) 
--insert into POPSERVER_4 VALUES ('MC310', 4, 'N', 0,  'Y', NULL, 'N', GETDATE()) 
--insert into POPSERVER_1 VALUES ('MC310', 6, 'Y', 88, 'N', NULL, 'N', GETDATE()) 



SELECT * FROM [HYUNDAI_TRANSYS_Gyeongju].[dbo].[JobHistory]
WHERE wmsid>0
--WHERE CallType = 'N'
--WHERE LEN(PartCD)>0
--WHERE wmsid>0 and jobState = 'JobDone'
--ORDER BY CallTime


SELECT CONVERT(date,JobFinishTime) [JobFinishDate], SUBSTRING(CallName,1,5) [LineCD], ResultCD, COUNT(*) [JobCount]
FROM JobHistory 
WHERE CallName LIKE 'MC1%' AND (JobFinishTime >'2022-10-01' and JobFinishTime <'2022-10-15') 
GROUP BY CONVERT(date,JobFinishTime), SUBSTRING(CallName,1,5), ResultCD
ORDER BY [JobFinishDate]

SELECT CONVERT(date,JobFinishTime) [JobFinishDate], SUBSTRING(CallName,1,5) [LineCD], ResultCD, COUNT(*) [JobCount]
FROM JobHistory 
WHERE CallName LIKE 'MC1%' AND (JobFinishTime >'2022-10-01' and JobFinishTime <'2022-10-15') and ResultCD=1
GROUP BY CONVERT(date,JobFinishTime), SUBSTRING(CallName,1,5), ResultCD
ORDER BY [JobFinishDate]


select SUBSTRING(CallName,1,5) [LineCD], count(*) [JobCount], JobState [result] from JobHistory 
where (CallName LIKE 'MC%') AND JobFinishTime >'2022-10-14' and JobFinishTime <'2022-10-15' and substring(callname,1,2) = 'mc' --and JobState <> 'jobdone'
group by SUBSTRING(CallName,1,5), JobState
order by SUBSTRING(CallName,1,5)


SELECT * FROM JobHistory 
WHERE (JobFinishTime >= '2022-10-1' AND JobFinishTime < '2022-10-15') and LineName LIKE 'BDM%'


SELECT SUBSTRING(CallName,1,5),COUNT(*) [JobCount]  FROM JobHistory 
WHERE (JobFinishTime >= '2022-10-14 06:00:00' AND JobFinishTime < '2022-10-15 06:00:00')
GROUP BY SUBSTRING(CallName,1,5)


SELECT SUBSTRING(CallName,1,5) [LineCD], ResultCD, COUNT(*) [JobCount]  FROM JobHistory 
WHERE (CallName LIKE 'MC%') AND (JobFinishTime >= '2022-10-14 06:00:00' AND JobFinishTime < '2022-10-15 06:00:00') AND ResultCD IS NOT NULL
GROUP BY SUBSTRING(CallName,1,5), ResultCD
ORDER BY LineCD


SELECT SUBSTRING(CallName,1,5) [LineCD], LineName, ResultCD, COUNT(*) [JobCount]  FROM JobHistory 
WHERE (CallName LIKE 'MC%') AND (JobFinishTime >= '2022-10-14 06:00:00' AND JobFinishTime < '2022-10-15 06:00:00')
GROUP BY SUBSTRING(CallName,1,5), LineName, ResultCD
ORDER BY LineCD


SELECT count(*)  FROM JobHistory 
WHERE  LineName LIKE 'BDM%' AND (JobFinishTime >= '2022-10-14' AND JobFinishTime < '2022-10-14 15:54:00') and JobState='jobdone'

SELECT * FROM JobHistory 
WHERE  LineName LIKE 'BDM%' AND (JobFinishTime >= '2022-10-14' AND JobFinishTime < '2022-10-14 15:54:00') and JobState='jobdone'


SELECT CONVERT(date,JobFinishTime) [date], LineName [LineNameLong], SUBSTRING(LineName, 1, CHARINDEX('_', LineName)-1) [LineNameShort] FROM JobHistory 
WHERE CHARINDEX('_', LineName) > 0 AND SUBSTRING(LineName,1,7) = 'MANUAL2' AND (JobFinishTime >= '2022-10-13' AND JobFinishTime < '2022-10-14') 

SELECT CONVERT(date,JobFinishTime) [date], SUBSTRING(LineName, 1, CHARINDEX('_', LineName)-1) [LineNameShort] FROM JobHistory 
WHERE CHARINDEX('_', LineName) > 0 AND (JobFinishTime >= '2022-10-13' AND JobFinishTime < '2022-10-14') 
