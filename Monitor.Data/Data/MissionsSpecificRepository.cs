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

        public MissionsSpecificRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public MissionsSpecific Add(MissionsSpecific model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO Missions_Specific
                               ([RobotGroup]
                               ,[RobotName]
                               ,[CallName]
                               ,[CallState]
                               ,[ACSState])
                           VALUES
                               (@RobotGroup
                               ,@RobotName
                               ,@CallName
                               ,@CallState
                               ,@ACSState);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                con.ExecuteScalar<int>(INSERT_SQL, param: model);
                logger.Info($"MissionSpecific Add   : {model}");
                return model;
            }
        }

        public IEnumerable<MissionsSpecific> GetAll()
        {
            using (var con = new SqlConnection(connectionString))
            {
                return con.Query<MissionsSpecific>("SELECT * FROM Missions_Specific");
            }
        }

        public void Update(MissionsSpecific model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string query = @"
                    UPDATE Missions_Specific
                    SET 
                        RobotGroup  = @RobotGroup ,
                        RobotName   = @RobotName  ,
                        CallName    = @CallName   ,
                        CallState   = @CallState  ,
                        ACSState    = @ACSState       
                    WHERE Id=@Id";

                con.Execute(query, param: model);
            }
        }

        public void Remove(MissionsSpecific model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Execute("DELETE FROM Missions_Specific WHERE ACSState=@ACSState", param: new { ACSState = model.ACSState });
                logger.Info($"Missions_Specific Remove: {model}");
            }
        }
    }
}
