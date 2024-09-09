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
    public class MissionRepository
    {
        private readonly static ILog logger = LogManager.GetLogger("MissionEvent");

        private readonly IDbConnection db;
        private readonly string connectionString = null;
        private readonly List<Mission> _missions = new List<Mission>(); // cached data

        public bool NeedUpdateUI { get; set; }

        public MissionRepository(string connectionString, RobotRepository robots)
        {
            this.connectionString = connectionString;
        }

        // DB에서 모든 항목을 로드하여 _missions 에 캐싱해 둔다

        public Mission Add(Mission model)
        {
            lock (this)
            {
                _missions.Add(model);
                NeedUpdateUI = true;

                using (var con = new SqlConnection(connectionString))
                {
                    const string INSERT_SQL = @"
                    INSERT INTO Missions
                               ([JobId]
                               ,[ACSMissionGroup]
                               ,[CallName]
                               ,[CallButtonIndex]
                               ,[CallButtonName]
                               ,[MissionName]
                               ,[ErrorMissionName]
                               ,[MissionOrderTime]
                               ,[JobCreateRobotName]
                               ,[RobotName]
                               ,[RobotID]
                               ,[ReturnID]
                               ,[MissionState])
                           VALUES
                               (@JobId
                               ,@ACSMissionGroup
                               ,@CallName
                               ,@CallButtonIndex
                               ,@CallButtonName
                               ,@MissionName
                               ,@ErrorMissionName
                               ,@MissionOrderTime
                               ,@JobCreateRobotName
                               ,@RobotName
                               ,@RobotID
                               ,@ReturnID
                               ,@MissionState);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                    model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                    logger.Info($"Mission Add   : {model}");
                    return model;
                }
            }
        }

        public List<Mission> Find(Func<Mission, bool> predicate)
        {
            lock (this)
            {
                return _missions.Where(predicate).ToList();
            }
        }

        public List<Mission> GetAll()
        {
            lock (this)
            {
                return _missions.ToList();
                //using (var con = new SqlConnection(connectionString))
                //{
                //    return con.Query<Mission>("SELECT * FROM Missions").ToList();
                //}
            }
        }
        public List<Mission> DBLoad()
        {
            _missions.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var job in con.Query<Mission>("SELECT * FROM Missions"))
                {
                    _missions.Add(job);
                }
                return _missions.ToList();
            }
        }

        public List<Mission> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<Mission>("SELECT * FROM Missions").ToList();
                }
            }
        }

        public Mission GetById(int id)
        {
            lock (this)
            {
                return _missions.SingleOrDefault(m => m.Id == id);
                //using (var con = new SqlConnection(connectionString))
                //{
                //    return con.Query<Mission>("SELECT * FROM Missions WHERE Id=@id",
                //        param: new { id = id }).FirstOrDefault();
                //}
            }
        }

        public Mission GetByCallButtonName(string aCSMissionName)
        {
            lock (this)
            {
                return _missions.FirstOrDefault(m => m.CallName == aCSMissionName);
                //using (var con = new SqlConnection(connectionString))
                //{
                //    return con.Query<Mission>("SELECT * FROM Missions WHERE CallButtonName=@callButtonName",
                //        param: new { callButtonName = callButtonName }).FirstOrDefault();
                //}
            }
        }


        public void Remove(Mission model)
        {
            lock (this)
            {
                _missions.Remove(model);
                NeedUpdateUI = true;

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM Missions WHERE Id=@id",
                        param: new { id = model.Id });
                    logger.Info($"Mission Remove: {model}");
                }
            }
        }

        public void Update(Mission model)
        {
            lock (this)
            {
                NeedUpdateUI = true;

                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE Missions 
                    SET 
                        JobId=@JobId,
                        ACSMissionGroup=@ACSMissionGroup,
                        CallName=@CallName,
                        CallButtonIndex=@CallButtonIndex, 
                        CallButtonName=@CallButtonName, 
                        MissionName=@MissionName, 
                        ErrorMissionName=@ErrorMissionName, 
                        MissionOrderTime=@MissionOrderTime, 
                        JobCreateRobotName=@JobCreateRobotName,
                        RobotName=@RobotName, 
                        RobotID=@RobotID, 
                        ReturnID=@ReturnID, 
                        MissionState=@MissionState 
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);
                    logger.Info($"Mission Update: {model}");
                }
            }
        }
        public void Delete()
        {
            lock (this)
            {
                _missions.Clear();
                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM Missions");
                }
            }
        }
        //public void UpdateState(Mission model)
        //{
        //    Mission missionToUpdate = _missions.SingleOrDefault(m => m.Id == model.Id);
        //    missionToUpdate.MissionState = model.MissionState;
        //    NeedUpdateUI = true;

        //    using (var con = new SqlConnection(connectionString))
        //    {
        //        con.Execute("UPDATE Missions SET MissionState=@missionState WHERE Id=@id",
        //            param: new { missionState = model.MissionState, id = model.Id });
        //    }
        //}
    }
}
