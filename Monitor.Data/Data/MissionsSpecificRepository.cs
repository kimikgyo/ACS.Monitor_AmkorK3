using Dapper;
using log4net;
using Monitor.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Data
{
    public class MissionsSpecificRepository
    {
        private readonly static ILog logger = LogManager.GetLogger("Missions_Specific");
        private readonly string connectionString = null;

        private readonly List<MissionsSpecific> _missionsSpecific = new List<MissionsSpecific>(); // cached data

        private readonly static object lockObj = new object();

        public MissionsSpecificRepository(string connectionString)
        {
            this.connectionString = connectionString;
            DBGetAll();
        }

        public MissionsSpecific Add(MissionsSpecific model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO Missions_Specific
                               ([RobotAlias]
                               ,[RobotName]
                               ,[CallName]
                               ,[CallState]
                               ,[JobSection]
                               ,[CallTime]
                               ,[Cancel]
                               ,[Priority])
                           VALUES
                               (@RobotAlias
                               ,@RobotName
                               ,@CallName
                               ,@CallState
                               ,@JobSection
                               ,@CallTime
                               ,@Cancel
                               ,@Priority);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                con.ExecuteScalar<int>(INSERT_SQL, param: model);
                logger.Info($"MissionSpecific Add   : {model}");
                return model;
            }
        }

        public List<MissionsSpecific> DBGetAll()
        {
            lock (lockObj)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<MissionsSpecific>("SELECT * FROM Missions_Specific").ToList();
                    con.Close();
                }

            }
        }

        public void Update(MissionsSpecific model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string query = @"
                    UPDATE Missions_Specific
                    SET 
                        RobotAlias = @RobotAlias ,
                        RobotName  = @RobotName ,
                        CallName   = @CallName  ,
                        CallState  = @CallState   ,
                        JobSection  = @JobSection   ,
                        CallTime   = @CallTime  ,
                        Cancel   = @Cancel  ,
                        Priority   = @Priority       
                    WHERE No=@No";

                con.Execute(query, param: model);
            }
        }

        public void Remove(MissionsSpecific model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Execute("DELETE FROM Missions_Specific WHERE ACSState=@ACSState", param: new { ACSState = model.CallState });
                logger.Info($"Missions_Specific Remove: {model}");
            }
        }
    }
}
