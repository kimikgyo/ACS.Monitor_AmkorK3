USE [master]
GO
/****** Object:  Database [AmkorK5_T3F_M3F]    Script Date: 2024-05-30 오전 11:12:32 ******/
CREATE DATABASE [AmkorK5_T3F_M3F]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AmkorK5_T3F_M3F', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\AmkorK5_T3F_M3F.mdf' , SIZE = 73728KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AmkorK5_T3F_M3F_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\AmkorK5_T3F_M3F_log.ldf' , SIZE = 466944KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AmkorK5_T3F_M3F].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET ARITHABORT OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET  DISABLE_BROKER 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET  MULTI_USER 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET QUERY_STORE = OFF
GO
USE [AmkorK5_T3F_M3F]
GO
/****** Object:  User [rhs]    Script Date: 2024-05-30 오전 11:12:32 ******/
CREATE USER [rhs] FOR LOGIN [rhs] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [monitor]    Script Date: 2024-05-30 오전 11:12:32 ******/
CREATE USER [monitor] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_datareader] ADD MEMBER [monitor]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [monitor]
GO
/****** Object:  Table [dbo].[Robots]    Script Date: 2024-05-30 오전 11:12:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Robots](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NULL,
	[ACSRobotGroup] [varchar](50) NULL,
	[ACSRobotActive] [int] NULL,
	[Fleet_State] [int] NULL,
	[Fleet_State_Text] [varchar](50) NULL,
	[RobotID] [int] NULL,
	[RobotIp] [varchar](50) NULL,
	[RobotName] [varchar](50) NULL,
	[StateID] [varchar](50) NULL,
	[StateText] [varchar](50) NULL,
	[MissionText] [varchar](500) NULL,
	[MissionQueueID] [int] NULL,
	[MapID] [varchar](50) NULL,
	[BatteryPercent] [float] NULL,
	[DistanceToNextTarget] [float] NULL,
	[Position_Orientation] [float] NULL,
	[Position_X] [float] NULL,
	[Position_Y] [float] NULL,
	[Product] [varchar](50) NULL,
	[Door] [varchar](50) NULL,
	[PositionZoneName] [varchar](50) NULL,
	[ErrorsJson] [varchar](5000) NULL,
	[RobotModel] [varchar](500) NULL,
	[RobotAlias] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[RobotInfo]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[RobotInfo]
as 
select JobId,RobotID,RobotName [Name],ACSRobotGroup [Group],ACSRobotActive [Active],Fleet_State_Text [FleetState],StateText [RobotState],Position_X [X],Position_Y [Y],Product 
from Robots
GO
/****** Object:  Table [dbo].[ACSChargerCountConfig]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSChargerCountConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChargerCountUse] [varchar](50) NULL,
	[RobotGroupName] [varchar](50) NULL,
	[FloorName] [varchar](50) NULL,
	[FloorMapId] [varchar](50) NULL,
	[ChargerGroupName] [varchar](50) NULL,
	[ChargerCount] [int] NULL,
	[ChargerCountStatus] [int] NULL,
	[DisplayFlag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ACSGroupConfigs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSGroupConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupUse] [varchar](50) NULL,
	[GroupName] [varchar](50) NULL,
	[DisplayFlag] [int] NULL,
 CONSTRAINT [PK_ACSRobotGroupModel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ACSModeInfo]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACSModeInfo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Location] [varchar](50) NULL,
	[ACSMode] [varchar](50) NULL,
	[ElevatorMode] [varchar](50) NULL,
 CONSTRAINT [PK_ElevatorMode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CallButtons]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CallButtons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ButtonIndex] [int] NULL,
	[ButtonName] [varchar](50) NULL,
	[IpAddress] [varchar](50) NULL,
	[ConnectionState] [varchar](50) NULL,
	[LastAccessTime] [datetime] NULL,
	[AccessElapsedTime] [float] NULL,
	[MissionCount] [int] NULL,
	[MissionStateText] [varchar](50) NULL,
 CONSTRAINT [PK_CallButtons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChargeMissionConfigs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChargeMissionConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ChargerGroupName] [varchar](50) NULL,
	[PositionZone] [varchar](50) NULL,
	[ChargeMissionUse] [varchar](50) NULL,
	[ChargeMissionName] [varchar](50) NULL,
	[StartBattery] [float] NULL,
	[SwitchaingBattery] [float] NULL,
	[EndBattery] [float] NULL,
	[ProductValue] [int] NULL,
	[ProductActive] [int] NULL,
	[RobotName] [varchar](50) NULL,
	[DisplayFlag] [int] NULL,
 CONSTRAINT [PK_ChargeMissionConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomMaps]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomMaps](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UpdateTime] [datetime] NULL,
	[MapName] [varchar](50) NULL,
	[MapImageData] [varchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ElevatorState]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ElevatorState](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RobotName] [varchar](50) NULL,
	[MirStateElevator] [varchar](50) NULL,
	[ElevatorState] [varchar](50) NULL,
	[ElevatorMissionName] [varchar](50) NULL,
 CONSTRAINT [PK_ElevatorState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquipmentCallNames]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentCallNames](
	[GROUP_NAME] [varchar](50) NOT NULL,
	[EQP_NAME] [varchar](50) NOT NULL,
	[INCH_TYPE] [varchar](50) NOT NULL,
	[CALL_NAME] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquipmentOrders]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentOrders](
	[Id] [int] NOT NULL,
	[EQP_NAME] [varchar](50) NOT NULL,
	[COMMAND] [varchar](50) NOT NULL,
	[INCH_TYPE] [varchar](50) NOT NULL,
	[IF_FLAG] [varchar](50) NOT NULL,
	[CREATE_DT] [datetime] NOT NULL,
	[MODIFY_DT] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EquipmentStatus]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EquipmentStatus](
	[Id] [int] NOT NULL,
	[EQP_NAME] [varchar](50) NOT NULL,
	[EQP_MODE] [int] NOT NULL,
	[PORT_ACCESS] [int] NOT NULL,
	[PORT_STATUS] [int] NOT NULL,
	[MODIFY_DT] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorCodeList]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorCodeList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ErrorCode] [int] NOT NULL,
	[ErrorMessage] [varchar](500) NULL,
	[ErrorType] [varchar](50) NULL,
	[Explanation] [varchar](500) NULL,
	[MailSend] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ErrorHistory]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ErrorHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ErrorCreateTime] [datetime] NULL,
	[RobotName] [varchar](50) NULL,
	[ErrorCode] [int] NULL,
	[ErrorDescription] [varchar](5000) NULL,
	[ErrorModule] [varchar](500) NULL,
	[ErrorMessage] [varchar](500) NULL,
	[ErrorType] [varchar](50) NULL,
	[Explanation] [varchar](500) NULL,
	[POSMessage] [varchar](500) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FloorMapIDConfigs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FloorMapIDConfigs](
	[Id] [int] NOT NULL,
	[FloorIndex] [nvarchar](50) NULL,
	[FloorName] [nvarchar](50) NULL,
	[MapID] [nvarchar](50) NULL,
	[Displayflag] [int] NOT NULL,
 CONSTRAINT [PK_FloorMapIDConfigs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobConfigs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobConfigUse] [varchar](50) NULL,
	[ACSMissionGroup] [varchar](50) NULL,
	[CallName] [varchar](50) NULL,
	[JobMissionName1] [varchar](50) NULL,
	[JobMissionName2] [varchar](50) NULL,
	[JobMissionName3] [varchar](50) NULL,
	[JobMissionName4] [varchar](50) NULL,
	[JobMissionName5] [varchar](50) NULL,
	[ExecuteBattery] [float] NULL,
	[jobCallSignal] [varchar](50) NULL,
	[jobCancelSignal] [varchar](50) NULL,
	[POSjobCallSignal_Reg32] [varchar](50) NULL,
	[POSjobCallSignal_Reg33] [varchar](50) NULL,
	[ProductValue] [int] NULL,
	[ProductActive] [int] NULL,
	[ElevatorModeValue] [varchar](50) NULL,
	[ElevatorModeActive] [int] NULL,
	[TransportCountActive] [int] NULL,
	[ErrorMissionName] [varchar](50) NULL,
	[MissionName] [varchar](50) NULL,
	[DisplayFlag] [int] NULL,
 CONSTRAINT [PK_JobConfigs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobHistory]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobHistory](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CallName] [varchar](50) NULL,
	[LineName] [varchar](50) NULL,
	[PostName] [varchar](50) NULL,
	[RobotName] [varchar](50) NULL,
	[JobState] [varchar](50) NULL,
	[CallTime] [datetime] NULL,
	[JobCreateTime] [datetime] NULL,
	[JobFinishTime] [datetime] NULL,
	[JobElapsedTime] [varchar](50) NULL,
	[MissionNames] [varchar](500) NULL,
	[MissionStates] [varchar](500) NULL,
	[ResultCD] [int] NULL,
 CONSTRAINT [PK_JobHistory] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jobs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jobs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[CallName] [varchar](500) NULL,
	[PopServerId] [int] NULL,
	[PopCallReturnType] [varchar](50) NULL,
	[PopCallAngle] [int] NULL,
	[PopCallState] [int] NULL,
	[WmsId] [int] NULL,
	[ACSJobGroup] [varchar](50) NULL,
	[JobCreateRobotName] [varchar](50) NULL,
	[RobotName] [varchar](50) NULL,
	[JobCreateTime] [datetime] NULL,
	[ExecuteBattery] [float] NULL,
	[JobState] [int] NULL,
	[JobStateText] [varchar](50) NULL,
	[MissionTotalCount] [int] NULL,
	[MissionSentCount] [int] NULL,
	[MissionId1] [varchar](50) NULL,
	[MissionId2] [varchar](50) NULL,
	[MissionId3] [varchar](50) NULL,
	[MissionId4] [varchar](50) NULL,
	[MissionId5] [varchar](50) NULL,
 CONSTRAINT [PK_Jobs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Missions]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Missions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[JobId] [int] NOT NULL,
	[ACSMissionGroup] [varchar](50) NULL,
	[CallName] [varchar](500) NULL,
	[CallButtonIndex] [int] NULL,
	[CallButtonName] [varchar](50) NULL,
	[MissionName] [varchar](50) NULL,
	[ErrorMissionName] [varchar](50) NULL,
	[MissionOrderTime] [datetime] NULL,
	[JobCreateRobotName] [varchar](50) NULL,
	[RobotName] [varchar](50) NULL,
	[RobotID] [int] NULL,
	[ReturnID] [int] NULL,
	[MissionState] [varchar](50) NULL,
 CONSTRAINT [PK_Missions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Missions_NonACS]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Missions_NonACS](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReturnID] [int] NULL,
	[RobotName] [varchar](50) NULL,
	[MissionName] [varchar](50) NULL,
	[MissionState] [varchar](50) NULL,
	[MissionOrderTime] [datetime] NULL,
	[MissionStartTime] [datetime] NULL,
	[MissionFinishTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Module]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Module](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[In_Value] [int] NULL,
	[Out_Value] [int] NULL,
	[DisplayName] [varchar](50) NULL,
 CONSTRAINT [PK_Module] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MonitorPcList]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MonitorPcList](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IpAddress] [varchar](50) NULL,
	[ZoneName] [varchar](50) NULL,
	[BcrExist] [int] NULL,
	[DisplayFlag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PannasonicPlcs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PannasonicPlcs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlcMapType] [varchar](50) NULL,
	[PlcMapName] [varchar](50) NULL,
	[PlcMapAddress] [varchar](50) NULL,
	[PlcMapMode] [varchar](50) NULL,
 CONSTRAINT [PK_PannasonicPlcs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PartModels]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LINE_CD] [varchar](50) NULL,
	[POST_CD] [int] NULL,
	[COMM_PO] [varchar](50) NULL,
	[OUT_Q] [int] NULL,
	[COMM_ANG] [int] NULL,
	[PART_CD] [varchar](50) NULL,
	[PART_NM] [varchar](50) NULL,
	[NP_MODE] [varchar](50) NULL,
	[NP_OUT_Q] [int] NULL,
	[NP_PART_CD] [varchar](50) NULL,
	[NP_PART_NM] [varchar](50) NULL,
 CONSTRAINT [PK_PartModelsTable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PlcConfigs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlcConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PlcModuleUse] [varchar](50) NULL,
	[PlcIpAddress] [varchar](50) NULL,
	[PortNumber] [int] NULL,
	[PlcModuleName] [varchar](50) NULL,
	[PlcMapType] [varchar](50) NULL,
	[ReadFirstMapAddress] [varchar](50) NULL,
	[ReadSecondMapAddress] [varchar](50) NULL,
	[WriteFirstMapAddress] [varchar](50) NULL,
	[WriteSecondMapAddress] [varchar](50) NULL,
	[ControlMode] [varchar](50) NULL,
	[DisplayFlag] [int] NULL,
 CONSTRAINT [PK_PlcConfigs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PositionAreaConfig]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PositionAreaConfig](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ACSRobotGroup] [varchar](50) NULL,
	[PositionAreaUse] [varchar](50) NULL,
	[PositionAreaFloorName] [varchar](50) NULL,
	[PositionAreaFloorMapId] [varchar](50) NULL,
	[PositionAreaName] [varchar](50) NULL,
	[PositionAreaX1] [varchar](50) NULL,
	[PositionAreaX2] [varchar](50) NULL,
	[PositionAreaX3] [varchar](50) NULL,
	[PositionAreaX4] [varchar](50) NULL,
	[PositionAreaY1] [varchar](50) NULL,
	[PositionAreaY2] [varchar](50) NULL,
	[PositionAreaY3] [varchar](50) NULL,
	[PositionAreaY4] [varchar](50) NULL,
	[DisplayFlag] [int] NULL,
 CONSTRAINT [PK_TrafficPositionAreaConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductNameInfoModel]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductNameInfoModel](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Regiser22Vlaue] [int] NULL,
	[RegisterNo] [varchar](50) NULL,
	[RegisterValue] [int] NULL,
	[ProductName] [varchar](50) NULL,
	[DisplayFlag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateTime] [datetime] NULL,
	[Barcode] [varchar](50) NULL,
	[RobotName] [varchar](50) NULL,
	[ProductName] [varchar](50) NULL,
	[Qty] [int] NULL,
	[Info1] [varchar](50) NULL,
	[Info2] [varchar](50) NULL,
	[Info3] [varchar](50) NULL,
	[Info4] [varchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegisterInfoModle]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisterInfoModle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ACSRobotGroup] [varchar](500) NULL,
	[RegisterNumber] [int] NULL,
	[RegisterValue] [int] NULL,
	[RegisterInfoMessge] [varchar](5000) NULL,
	[DisplayFlag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RobotRegisterSync]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RobotRegisterSync](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegisterSyncUse] [varchar](50) NULL,
	[PositionGroup] [varchar](50) NULL,
	[PositionName] [varchar](50) NULL,
	[ACSRobotGroup] [varchar](50) NULL,
	[RegisterNo] [int] NULL,
	[RegisterValue] [int] NULL,
	[DisplayFlag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Skynet_RobotData]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Skynet_RobotData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SkyNetMode] [varchar](50) NULL,
	[Linecode] [varchar](50) NULL,
	[Processcode] [varchar](50) NULL,
	[RobotName] [varchar](50) NULL,
	[RobotState] [varchar](50) NULL,
	[MissionName] [varchar](50) NULL,
	[MissionState] [varchar](50) NULL,
 CONSTRAINT [PK_Skynet_EM_DataSend] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WaitMissionConfigs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WaitMissionConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PositionZone] [varchar](50) NULL,
	[WaitMissionUse] [varchar](50) NULL,
	[WaitMissionName] [varchar](50) NULL,
	[EnableBattery] [float] NULL,
	[ProductValue] [int] NULL,
	[ProductActive] [int] NULL,
	[RobotName] [varchar](50) NULL,
	[DisplayFlag] [int] NULL,
 CONSTRAINT [PK_WaitMissionConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WiseModules]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WiseModules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ModuleUse] [varchar](50) NULL,
	[ModuleIpAddress] [varchar](50) NULL,
	[ModuleName] [varchar](50) NULL,
	[ModuleStatus] [varchar](50) NULL,
	[ModuleControlMode] [varchar](50) NULL,
	[ModuleIn_Value] [int] NULL,
	[ModuleOut_Value] [int] NULL,
	[DisplayName] [varchar](50) NULL,
	[DisplayFlag] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WiseTowerLampConfigs]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WiseTowerLampConfigs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NameSetting] [varchar](50) NULL,
	[PositionZoneSetting] [varchar](50) NULL,
	[ControlSetting] [varchar](50) NULL,
	[TowerLampUseSetting] [varchar](50) NULL,
	[IpAddressSetting] [varchar](50) NULL,
	[DisplayNameSetting] [varchar](50) NULL,
	[OperationtimeSetting] [int] NULL,
	[ProductValueSetting] [int] NULL,
	[ProductActiveSetting] [int] NULL,
	[ProductName] [varchar](50) NULL,
	[Register22ValueSetting] [varchar](50) NULL,
	[Register32ValueSetting] [varchar](50) NULL,
	[Register33ValueSetting] [varchar](50) NULL,
	[Register34ValueSetting] [varchar](50) NULL,
	[Register35ValueSetting] [varchar](50) NULL,
	[DisplayFlag] [int] NULL,
 CONSTRAINT [PK_EtnLampConfigs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WMSModels]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WMSModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LINE_CD] [varchar](50) NULL,
	[POST_CD] [int] NULL,
	[COMM_ANG] [int] NULL,
	[RETU_TYPE] [varchar](50) NULL,
	[OUT_Q] [int] NULL,
	[PART_CD] [varchar](50) NULL,
	[PART_NM] [varchar](50) NULL,
	[OUT_WH] [varchar](50) NULL,
	[OUT_POINT] [int] NULL,
	[WMS_IF_FLAG] [varchar](50) NULL,
	[CREATE_DT] [datetime] NULL,
	[MODIFY_DT] [datetime] NULL,
 CONSTRAINT [PK_WMSModels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[EquipmentOrders] ADD  DEFAULT (getdate()) FOR [CREATE_DT]
GO
ALTER TABLE [dbo].[EquipmentOrders] ADD  DEFAULT (getdate()) FOR [MODIFY_DT]
GO
ALTER TABLE [dbo].[EquipmentStatus] ADD  DEFAULT (getdate()) FOR [MODIFY_DT]
GO
/****** Object:  StoredProcedure [dbo].[spGetErrorHistoryAggr1]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetErrorHistoryAggr1]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotNamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--date 테이블 생성
	create table #d_date (d_type datetime)
	declare @var datetime = @searchDate1
	while(@var < @searchDate2)
	begin
		insert into #d_date values(@var)
		set @var = DATEADD(DAY, +1, @var)
	end


	-- 날짜별 에러발생건수
	SELECT FORMAT(A.d_type, 'yyyy-MM-dd') [DATE], COUNT(B.에러발생일자) [TOTAL], COUNT(IIF(B.SHIFT=1,1,NULL)) [OFFICE], COUNT(IIF(B.SHIFT=0,1,NULL)) [NIGHT]
	FROM
	(
		SELECT d_type FROM #d_date
	) A
	LEFT JOIN
	(
		SELECT 
			FORMAT(ErrorCreateTime, 'yyyy-MM-dd') [에러발생일자],
			IIF(FORMAT(ErrorCreateTime, 'HH:mm:ss') BETWEEN '08:30:00' AND '17:30:00', 1, 0) [SHIFT]
		FROM [ErrorHistory] WITH(NOLOCK)
		WHERE 
			(ErrorCreateTime >= @searchDate1 AND ErrorCreateTime < @searchDate2)
			AND ErrorCode >= 0
			AND (
				(@robotNamesEmpty = 1)
				OR
				(@robotNamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
			)
	) B
	ON A.d_type = B.에러발생일자
	GROUP BY A.d_type
	ORDER BY A.d_type


	--날짜별 반송량
	SELECT FORMAT(A.d_type, 'yyyy-MM-dd') [날짜], COUNT(B.JOB완료일자) [반송량]
	FROM
	(
		SELECT d_type FROM #d_date
	) A
	LEFT JOIN
	(
		SELECT 
			FORMAT(JobFinishTime, 'yyyy-MM-dd') [JOB완료일자]
		FROM [JobHistory] WITH(NOLOCK)
		WHERE 
			(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
			AND ResultCD = 11 
			AND LEN(RobotName) > 0
			AND 
			(
				(@robotNamesEmpty = 1)
				OR
				(@robotNamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
			)
	) B
	ON A.d_type = B.JOB완료일자
	GROUP BY A.d_type
	ORDER BY A.d_type

END
GO
/****** Object:  StoredProcedure [dbo].[spGetErrorHistoryAggr2]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spGetErrorHistoryAggr2]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotNamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--date 테이블 생성
	create table #d_date (d_type datetime)
	declare @var datetime = @searchDate1
	while(@var < @searchDate2)
	begin
		insert into #d_date values(@var)
		set @var = DATEADD(DAY, +1, @var)
	end


	--에러별 발생건수
	SELECT COUNT(*) [에러개수], ErrorCode, ErrorMessage [ErrorText]
	FROM [ErrorHistory] WITH(NOLOCK)
	WHERE 
		(ErrorCreateTime >= @searchDate1 AND ErrorCreateTime < @searchDate2)
		AND ErrorCode >= 0
		AND LEN(RobotName) > 0
		AND 
		(
			(@robotNamesEmpty = 1)
			OR
			(@robotNamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
		)
	GROUP BY ErrorCode, ErrorMessage
	ORDER BY [에러개수] DESC, ErrorCode

END
GO
/****** Object:  StoredProcedure [dbo].[spGetJobHistoryAggr1]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetJobHistoryAggr1]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames  varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotNamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--로봇별 반송량&반송시간
	SELECT 
	--  ROW_NUMBER() OVER(ORDER BY RobotName ASC) [No], 
		RobotName, COUNT(*) [반송량], AVG(datediff(SECOND,JobCreateTime,JobFinishTime)) [평균반송시간]
	FROM [JobHistory] WITH(NOLOCK)
	WHERE 
		(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
		AND ResultCD = 11
		AND LEN(RobotName) > 0
		AND 
		(
			(@robotNamesEmpty = 1)
			OR
			(@robotNamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
		)
	GROUP BY RobotName

END
GO
/****** Object:  StoredProcedure [dbo].[spGetJobHistoryAggr2]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetJobHistoryAggr2]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames  varchar(max),	--로봇목록(콤마구분)
	@lineNames   varchar(max)	--출발지목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotNamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)
	DECLARE @lineNamesEmpty  INT = IIF(@lineNames  IS NULL OR LEN(@lineNames)  = 0, 1, 0)


	--출발지별 반송량&반송시간
	SELECT LineName [출발지], COUNT(*) [반송량], AVG(datediff(SECOND,JobCreateTime,JobFinishTime)) [평균반송시간]
	FROM [JobHistory] WITH(NOLOCK)
	WHERE 
		(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
		AND ResultCD = 11 
		AND LEN(RobotName) > 0 
		AND 
		(
			(@robotNamesEmpty = 1)
			OR
			(@robotNamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
		)
		AND LEN(LineName) > 0 
		AND 
		(
			(@lineNamesEmpty = 1)
			OR
			(@lineNamesEmpty = 0 AND LineName IN (SELECT value FROM string_split(@lineNames, ',')))
		)
	GROUP BY LineName

END
GO
/****** Object:  StoredProcedure [dbo].[spGetJobHistoryAggr3]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetJobHistoryAggr3]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames  varchar(max),	--로봇목록(콤마구분)
	@postNames   varchar(max)	--목적지목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotNamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)
	DECLARE @postNamesEmpty  INT = IIF(@postNames  IS NULL OR LEN(@postNames)  = 0, 1, 0)


	--목적지별 반송량&반송시간
	SELECT PostName [목적지], COUNT(*) [반송량], AVG(datediff(SECOND,JobCreateTime,JobFinishTime)) [평균반송시간]
	FROM [JobHistory] WITH(NOLOCK)
	WHERE 
		(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
		AND ResultCD = 11 
		AND LEN(RobotName) > 0 
		AND 
		(
			(@robotNamesEmpty = 1)
			OR
			(@robotNamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
		)
		AND LEN(PostName) > 0 
		AND 
		(
			(@postNamesEmpty = 1)
			OR
			(@postNamesEmpty = 0 AND PostName IN (SELECT value FROM string_split(@postNames, ',')))
		)
	GROUP BY PostName

END
GO
/****** Object:  StoredProcedure [dbo].[spGetJobHistoryAggr4]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetJobHistoryAggr4]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames  varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotNamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--시간별 반송량
	SELECT DATEPART(HOUR, JobFinishTime) [Hour], COUNT(*) [반송량]
	FROM [JobHistory] WITH(NOLOCK)
	WHERE 
		(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
		AND ResultCD = 11 
		AND LEN(RobotName) > 0 
		AND 
		(
			(@robotNamesEmpty = 1)
			OR
			(@robotNamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
		)
	GROUP BY ALL DATEPART(HOUR, JobFinishTime)
	ORDER BY DATEPART(HOUR, JobFinishTime)

END
GO
/****** Object:  StoredProcedure [dbo].[spGetSummary1_총반송량_평균반송시간]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSummary1_총반송량_평균반송시간]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotnamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--총반송량, 반송시간(최대/최소/평균/편차)
	SELECT 
		COUNT(*) [총반송량],
		MAX(datediff(SECOND,JobCreateTime,JobFinishTime)) [반송시간MAX],
		MIN(datediff(SECOND,JobCreateTime,JobFinishTime)) [반송시간MIN],
		AVG(datediff(SECOND,JobCreateTime,JobFinishTime)) [반송시간AVG],
		STDEVP(datediff(SECOND,JobCreateTime,JobFinishTime)) [반송시간STDEVP]
	FROM [JobHistory]
	WHERE 
		(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
		AND ResultCD = 11
		AND LEN(RobotName) > 0
		AND 
		(
			(@robotnamesEmpty = 1 AND LEN(RobotName) > 0)
			OR
			(@robotnamesEmpty = 0 AND LEN(RobotName) > 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
		)

END
GO
/****** Object:  StoredProcedure [dbo].[spGetSummary2_시간평균반송량]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSummary2_시간평균반송량]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotnamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--시간평균반송량
	SELECT 
		COUNT(*) [시간갯수], 
		SUM(A.[반송량]) [총반송량], 
		ROUND(AVG(Cast(A.[반송량] AS Float)),2) [시간평균반송량]
	FROM
	(
		--시간대별 반송량
		SELECT 
			DATEPART(HOUR, JobFinishTime) [Hour], 
			COUNT(*) [반송량]
		FROM [JobHistory]
		WHERE 
			(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
			AND ResultCD = 11 
			AND LEN(RobotName) > 0 
			AND
			(
				(@robotnamesEmpty = 1 AND LEN(RobotName) > 0)
				OR
				(@robotnamesEmpty = 0 AND LEN(RobotName) > 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
			)
		GROUP BY ALL DATEPART(HOUR, JobFinishTime)
	) A

END
GO
/****** Object:  StoredProcedure [dbo].[spGetSummary3_월평균반송량]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSummary3_월평균반송량]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotnamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--월평균반송량
	SELECT 
		COUNT(*) [월수], 
		SUM(A.[반송량]) [총반송량], 
		AVG(Cast(A.[반송량] AS Float)) [월평균반송량]
	FROM
	(
		--월별반송량
		SELECT 
			DATEPART(MONTH, JobFinishTime) [Month], 
			COUNT(*) [반송량]
		FROM [JobHistory]
		WHERE 
			(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
			AND ResultCD = 11 
			AND
			(
				(@robotnamesEmpty = 1)
				OR
				(@robotnamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
			)
		GROUP BY ALL DATEPART(MONTH, JobFinishTime)
	) A

END
GO
/****** Object:  StoredProcedure [dbo].[spGetSummary4_총에러수_평균반송시간]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSummary4_총에러수_평균반송시간]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotnamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--총에러수, 반송시간(최대/최소/평균/편차)
	SELECT 
		COUNT(*) [총에러수]
	FROM [ErrorHistory]
	WHERE 
		(ErrorCreateTime >= @searchDate1 AND ErrorCreateTime < @searchDate2)
		AND LEN(RobotName) > 0
		AND
		(
			(@robotnamesEmpty = 1)
			OR
			(@robotnamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
		)

END
GO
/****** Object:  StoredProcedure [dbo].[spGetSummary5_시간평균에러수]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSummary5_시간평균에러수]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime,		--종료날짜
	@robotNames varchar(max)	--로봇목록(콤마구분)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @robotnamesEmpty INT = IIF(@robotNames IS NULL OR LEN(@robotNames) = 0, 1, 0)


	--시간평균에러수
	SELECT 
		COUNT(*) [시간갯수], 
		SUM(A.[에러수]) [총에러수], 
		ROUND(AVG(Cast(A.[에러수] AS Float)),2) [시간평균에러수]
	FROM
	(
		--시간대별 에러수
		SELECT 
			DATEPART(HOUR, ErrorCreateTime) [Hour], 
			COUNT(*) [에러수]
		FROM [ErrorHistory]
		WHERE 
			(ErrorCreateTime >= @searchDate1 AND ErrorCreateTime < @searchDate2)
			AND LEN(RobotName) > 0
			AND
			(
				(@robotnamesEmpty = 1)
				OR
				(@robotnamesEmpty = 0 AND RobotName IN (SELECT value FROM string_split(@robotNames, ',')))
			)
		GROUP BY ALL DATEPART(HOUR, ErrorCreateTime)
	) A

END
GO
/****** Object:  StoredProcedure [dbo].[spGetSummary6_월별반송량]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetSummary6_월별반송량]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime		--종료날짜
AS
BEGIN
	SET NOCOUNT ON;


	--월별 반송량
	SELECT concat(DATEPART(MONTH, JobFinishTime), '월') [Month], COUNT(*) [반송량]
	FROM [JobHistory]
	WHERE 
		(JobFinishTime >= @searchDate1 AND JobFinishTime < @searchDate2)
		AND ResultCD = 11  
	GROUP BY DATEPART(MONTH, JobFinishTime)
	ORDER BY DATEPART(MONTH, JobFinishTime)

END
GO
/****** Object:  StoredProcedure [dbo].[spSub1]    Script Date: 2024-05-30 오전 11:12:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[spSub1]
	@searchDate1 datetime,		--시작날짜
	@searchDate2 datetime		--종료날짜
AS
BEGIN
	SET NOCOUNT ON;

	----month 테이블 생성
	--drop table #months
	--create table #months (dates datetime)
	--declare @var datetime = @searchDate1
	--while @var < dateadd(MONTH, +1, @searchDate2)
	--begin
	--	insert into #months Values(@var)
	--	set @var = Dateadd(MONTH, +1, @var)
	--end

	----date 테이블 생성
	--create table #d_date (d_type datetime)
	--declare @var datetime = @searchDate1
	--while(@var < @searchDate2)
	--begin
	--	insert into #d_date values(@var)
	--	set @var = DATEADD(DAY, +1, @var)
	--end

END
GO
USE [master]
GO
ALTER DATABASE [AmkorK5_T3F_M3F] SET  READ_WRITE 
GO
