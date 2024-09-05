using Dapper;
using log4net;
using Monitor.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Data
{
    public class PositionAreaConfigRepository
    {
        //private readonly static ILog logger = LogManager.GetLogger("User");

        private readonly IDbConnection db;
        private readonly string connectionString = null;

        private readonly List<PositionAreaConfig> _PositionAreaConfig = new List<PositionAreaConfig>(); // cache data


        public PositionAreaConfigRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }
      
        private void Load()
        {
            _PositionAreaConfig.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var positionAreaConfig in con.Query<PositionAreaConfig>("SELECT * FROM PositionAreaConfig WHERE DisplayFlag=1"))
                {

                    _PositionAreaConfig.Add(positionAreaConfig);
                }
            }
        }

        public List<PositionAreaConfig> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<PositionAreaConfig>("SELECT * FROM PositionAreaConfig WHERE DisplayFlag=1").ToList();

                }
            }
        }

        //DB 추가하기
        public PositionAreaConfig Add(PositionAreaConfig model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO PositionAreaConfig
                                ([ACSRobotGroup]
                                ,[PositionAreaUse]
                                ,[PositionAreaFloorName]
                                ,[PositionAreaFloorMapId]
                                ,[PositionAreaName]
                                ,[PositionAreaX1]
                                ,[PositionAreaX2]
                                ,[PositionAreaX3]
                                ,[PositionAreaX4]
                                ,[PositionAreaY1]
                                ,[PositionAreaY2]
                                ,[PositionAreaY3]
                                ,[PositionAreaY4]
                                ,[PositionWaitTimeLog]
                                ,[DisplayFlag])
                           VALUES
                                (@ACSRobotGroup
                                ,@PositionAreaUse
                                ,@PositionAreaFloorName
                                ,@PositionAreaFloorMapId
                                ,@PositionAreaName
                                ,@PositionAreaX1
                                ,@PositionAreaX2
                                ,@PositionAreaX3
                                ,@PositionAreaX4
                                ,@PositionAreaY1
                                ,@PositionAreaY2
                                ,@PositionAreaY3
                                ,@PositionAreaY4
                                ,@PositionWaitTimeLog
                                ,@DisplayFlag);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                //logger.Info($"PositionAreaConfig Add   : {model}");
                return model;
            }
        }

        //DB찾기
        public List<PositionAreaConfig> Find(Func<PositionAreaConfig, bool> predicate)
        {
            lock (this)
            {
                return _PositionAreaConfig.Where(predicate).ToList();
            }
        }


        //DB업데이트
        public void Update(PositionAreaConfig model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE PositionAreaConfig 
                    SET 
                        ACSRobotGroup=@ACSRobotGroup, 
                        PositionAreaUse=@PositionAreaUse, 
                        PositionAreaFloorName=@PositionAreaFloorName, 
                        PositionAreaFloorMapId=@PositionAreaFloorMapId, 
                        PositionAreaName=@PositionAreaName, 
                        PositionAreaX1=@PositionAreaX1, 
                        PositionAreaX2=@PositionAreaX2,        
                        PositionAreaX3=@PositionAreaX3,
                        PositionAreaX4=@PositionAreaX4, 
                        PositionAreaY1=@PositionAreaY1,        
                        PositionAreaY2=@PositionAreaY2,
                        PositionAreaY3=@PositionAreaY3,
                        PositionAreaY4=@PositionAreaY4,
                        PositionWaitTimeLog=@PositionWaitTimeLog,
                        DisplayFlag=@DisplayFlag
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);
                    //logger.Info($"PositionAreaConfig Update: {model}");
                }
            }
        }


        //DB삭제
        public void Remove(PositionAreaConfig model)
        {
            lock (this)
            {
                _PositionAreaConfig.Remove(model);

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM PositionAreaConfig WHERE Id=@id",
                        param: new { id = model.Id });
                    //logger.Info($"PositionAreaConfig Remove: {model}");
                }
            }
        }
    }
}

