
using System;
using System.Collections.Generic;
using System.Data;

namespace Monitor.Data
{
    public class ConnectionStrings
    {
        // TRANSYS
        //public static readonly string DB1 = @"Data Source=.\SQLEXPRESS;Initial Catalog=HYUNDAI_TRANSYS_Gyeongju;Integrated Security=True;Connect Timeout=30;";
        //public static readonly string DB1 = @"Data Source=127.0.0.1,1433;Network Library=DBMSSOCN;Initial Catalog = HYUNDAI_TRANSYS_Gyeongju; User ID = sa; Password=pass6535;";
        //public static readonly string DB1 = @"Data Source=127.0.0.1,1433;Network Library=DBMSSOCN;Initial Catalog = HYUNDAI_TRANSYS_Gyeongju; User ID = test; Password=test;";

        // AMKOR-K5
#if LOCALDB

        public static readonly string DB1 = @"Data Source=.\SQLEXPRESS;Initial Catalog=AmkorK3_AMB;User ID = sa; Password=acsserver;Connect Timeout=30;"; // My(Kimikgyo) PC
        public static readonly string DB2 = @"Data Source=.\SQLEXPRESS;Initial Catalog=AmkorK3_AMB;User ID = sa; Password=acsserver;Connect Timeout=30;"; // My(Kimikgyo) PC
        public static readonly string DB3 = @"Data Source=.\SQLEXPRESS;Initial Catalog=AmkorK3_User_Data;User ID = sa; Password=acsserver;Connect Timeout=30;"; // My(Kimikgyo) PC


#elif TESTDB
        public static readonly string DB1 = @"Data Source=192.168.8.201;Initial Catalog=AmkorK3_AMB; User ID = sa; Password=acsserver;Connect Timeout=30;";
        public static readonly string DB2 = @"Data Source=192.168.8.201;Initial Catalog=AmkorK3_AMB; User ID = sa; Password=acsserver;Connect Timeout=30;";
        public static readonly string DB3 = @"Data Source=192.168.8.201;Initial Catalog=AmkorK3_User_Data; User ID = sa; Password=acsserver;Connect Timeout=30;";
#else
        // ACS PC만 연결
        public static readonly string DB1 = @"Data Source=10.141.26.108;Initial Catalog=AmkorK5_T3F_M3F;User ID = sa; Password=acsserver;Connect Timeout=30;";
        public static readonly string DB2 = @"Data Source=10.141.26.108;Initial Catalog=AmkorK5_T3F_M3F;User ID = sa; Password=acsserver;Connect Timeout=30;";
        public static readonly string DB3 = @"Data Source=10.141.26.108;Initial Catalog=AmkorK5_User_Data;User ID = sa; Password=acsserver;Connect Timeout=30;";
#endif

    }


    public interface IUnitOfWork : IDisposable
    {
        RobotRepository Robots { get; }

        MissionRepository Missions { get; }
        JobRepository Jobs { get; }
        PositionAreaConfigRepository PositionAreaConfigs { get; }

        ElevatorStateRepository ElevatorStates { get; }
        ProductNameInfoRepository ProductNameInfos { get; }

        JobConfigRepository JobConfigs { get; }
        ACSRobotGroupRepository ACSRobotGroupConfigs { get; }
        WaitMissionConfigRepository WaitMissionConfigs { get; }
        ChargeMissionConfigRepository ChargeMissionConfigs { get; }

        FleetPositionRepository FleetPositions { get; }
        FloorMapIDConfigRepository FloorMapIDConfigs { get; }
        ACSChargerCountConfigRepository ACSChargerCountConfigs { get; }
        RobotRegisterSyncRepository RobotRegisterSyncs { get; }
        ErrorCodeListRepository ErrorCodeList { get; }
        ElevatorInfoRepository ElevatorInfos { get; }
        JobLogRepository JobLogs { get; }
        ErrorHistoryRepository ErrorHistorys { get; }
        PositionWaitTimeHistoryRepository PositionWaitTimeHistorys { get; }
        PositionWaitTimeRepository PositionWaitTimes { get; }
        CustomMapsRepository CustomMaps { get; }

        MissionsSpecificRepository MissionsSpecifics { get; }

        UserChangeModeLogRepository UserChangeModeLogs { get; }
        UserEmailAddressRepository UserEmailAddress { get; }
        UserNumberRepositoy UserNumbers { get; }


        void SaveChanges();
        //void BeginTransaction();
        //bool Commit();
        //void Rollback();
        //IRepository<TEntity> Repository<TEntity>();
    }


    public class UnitOfWork : IUnitOfWork
    {
        private IDbConnection _db;

        private static readonly string connectionString = ConnectionStrings.DB1;
        private static readonly string connectionString2 = ConnectionStrings.DB2;
        private static readonly string connectionString3 = ConnectionStrings.DB3;

        // ================================================================ POP DB 설정
        // POP서버#1
        // - MC110 BDM 라인 레일/트랙
        // - MC350 파워 리클 1
        // connectionString = @"Data Source=10.214.253.21,1433;Network Library=DBMSSOCN;Initial Catalog = MSEAT_MES; User ID = from_mes; Password=srvMaster11;";
        //
        // POP서버#2
        // - MC112 NQ5a 레일/트랙
        // - MC351 파워 리클 2
        // - MC311 메뉴얼 리클 2
        // connectionString = @"Data Source=10.214.253.21,1433;Network Library=DBMSSOCN;Initial Catalog = MSEAT_MES_BDM; User ID = from_mes; Password=srvMaster11;";
        //
        // POP서버#3 레일
        // - MC111 QY2열
        // connectionString = @"Data Source=10.214.253.92,1433;Network Library=DBMSSOCN;Initial Catalog = IF_DB_TH; User ID = user; Password=user1234;";
        //
        // POP서버#4 리클
        // - MC310
        // connectionString = @"Data Source=10.214.253.93,1433;Network Library=DBMSSOCN;Initial Catalog = MES; User ID = intown; Password=intown;";
        // ================================================================ POP DB 설정

        const string popConnString1 = @"Data Source=10.214.253.21,1433;Network Library=DBMSSOCN;Initial Catalog = MSEAT_MES; User ID = from_mes; Password=srvMaster11;";
        const string popConnString2 = @"Data Source=10.214.253.21,1433;Network Library=DBMSSOCN;Initial Catalog = MSEAT_MES_BDM; User ID = from_mes; Password=srvMaster11;";
        const string popConnString3 = @"Data Source=10.214.253.92,1433;Network Library=DBMSSOCN;Initial Catalog = IF_DB_TH; User ID = user; Password=user1234;";
        const string popConnString4 = @"Data Source=10.214.253.93,1433;Network Library=DBMSSOCN;Initial Catalog = MES; User ID = intown; Password=intown;";



        public RobotRepository Robots { get; private set; }
        public JobRepository Jobs { get; private set; }
        public MissionRepository Missions { get; private set; }
        public PositionAreaConfigRepository PositionAreaConfigs { get; private set; }
        public ProductNameInfoRepository ProductNameInfos { get; private set; }
        public ElevatorStateRepository ElevatorStates { get; private set; }
        public JobConfigRepository JobConfigs { get; private set; }
        public ACSRobotGroupRepository ACSRobotGroupConfigs { get; private set; }
        public WaitMissionConfigRepository WaitMissionConfigs { get; private set; }
        public ChargeMissionConfigRepository ChargeMissionConfigs { get; private set; }
        public FleetPositionRepository FleetPositions { get; private set; }
        public FloorMapIDConfigRepository FloorMapIDConfigs { get; private set; }
        public ACSChargerCountConfigRepository ACSChargerCountConfigs { get; private set; }
        public RobotRegisterSyncRepository RobotRegisterSyncs { get; private set; }
        public ErrorCodeListRepository ErrorCodeList { get; private set; }
        public UserEmailAddressRepository UserEmailAddress { get; private set; }
        public UserNumberRepositoy UserNumbers { get; private set; }
        public ElevatorInfoRepository ElevatorInfos { get; private set; }
        public JobLogRepository JobLogs { get; private set; }
        public ErrorHistoryRepository ErrorHistorys { get; private set; }
        public PositionWaitTimeHistoryRepository PositionWaitTimeHistorys { get; private set; }
        public PositionWaitTimeRepository PositionWaitTimes { get; private set; }
        public MissionsSpecificRepository MissionsSpecifics { get; private set; }
        public UserChangeModeLogRepository UserChangeModeLogs { get; private set; }
        public CustomMapsRepository CustomMaps { get; private set; }
        //=========================================================

        public UnitOfWork()
        {
            Robots = new RobotRepository(connectionString);
            Missions = new MissionRepository(connectionString, Robots);
            Jobs = new JobRepository(connectionString, Missions);
            PositionAreaConfigs = new PositionAreaConfigRepository(connectionString);
            ElevatorStates = new ElevatorStateRepository(connectionString);
            ProductNameInfos = new ProductNameInfoRepository(connectionString);
            JobLogs = new JobLogRepository(connectionString);
            ErrorHistorys = new ErrorHistoryRepository(connectionString);

            JobConfigs = new JobConfigRepository(connectionString);
            ACSRobotGroupConfigs = new ACSRobotGroupRepository(connectionString);
            WaitMissionConfigs = new WaitMissionConfigRepository(connectionString);
            ChargeMissionConfigs = new ChargeMissionConfigRepository(connectionString);
            FleetPositions = new FleetPositionRepository(connectionString);
            FloorMapIDConfigs = new FloorMapIDConfigRepository(connectionString);
            ACSChargerCountConfigs = new ACSChargerCountConfigRepository(connectionString);
            RobotRegisterSyncs = new RobotRegisterSyncRepository(connectionString);
            ErrorCodeList = new ErrorCodeListRepository(connectionString);
            ElevatorInfos = new ElevatorInfoRepository(connectionString);
            PositionWaitTimeHistorys = new PositionWaitTimeHistoryRepository(connectionString);
            PositionWaitTimes = new PositionWaitTimeRepository(connectionString);
            MissionsSpecifics = new MissionsSpecificRepository(connectionString);
            CustomMaps = new CustomMapsRepository(connectionString);

            UserEmailAddress = new UserEmailAddressRepository(ConnectionStrings.DB3);
            UserNumbers = new UserNumberRepositoy(ConnectionStrings.DB3);
            UserChangeModeLogs = new UserChangeModeLogRepository(ConnectionStrings.DB3);

        }

        public void SaveChanges()
        {
            //begin transaction
            //...update robots
            //...update missions
            //commit/rollback
        }

        public void Dispose()
        {
            //_db.Dispose();
        }
    }
}
