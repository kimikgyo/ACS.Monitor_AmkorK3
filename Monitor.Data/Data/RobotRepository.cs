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
    public class RobotRepository
    {
        private readonly static ILog logger = LogManager.GetLogger("RobotEvent");

        private readonly IDbConnection db;
        private readonly string connectionString = null;
        private readonly List<Robot> _robots = new List<Robot>(); // cache data
        private readonly static object lockObj = new object();

        // 현재 로봇정보는 SystemConfig화면에서 Active,Group 설정할때만 저장된다. 프로그램 기동시에는 읽기만 한다.
        public RobotRepository(string connectionString)
        {
            this.connectionString = connectionString;
            DBLoad();
        }

        public List<Robot> DBLoad()
        {
            _robots.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var robot in con.Query<Robot>("SELECT * FROM Robots"))
                {

                    _robots.Add(robot);
                }
                return _robots.ToList();
            }
        }

        public List<Robot> Update()
        {
            lock (lockObj)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    foreach (var robots in con.Query<Robot>("SELECT * FROM Robots"))
                    {
                        var _robot = _robots.FirstOrDefault(x => x.Id == robots.Id/*&& x.RobotName == robots.RobotName*/);
                        if (_robot != null)
                        {
                            _robot.Id = robots.Id;
                            _robot.JobId = robots.JobId;
                            _robot.ACSRobotGroup = robots.ACSRobotGroup;
                            _robot.ACSRobotActive = robots.ACSRobotActive;
                            _robot.Fleet_State = robots.Fleet_State;
                            _robot.Fleet_State_Text = robots.Fleet_State_Text;
                            _robot.RobotID = robots.RobotID;
                            _robot.RobotIp = robots.RobotIp;
                            _robot.RobotName = robots.RobotName;
                            _robot.StateID = robots.StateID;
                            _robot.StateText = robots.StateText;
                            _robot.MissionText = robots.MissionText;
                            _robot.MissionQueueID = robots.MissionQueueID;
                            _robot.MapID = robots.MapID;
                            _robot.BatteryPercent = robots.BatteryPercent;
                            _robot.DistanceToNextTarget = robots.DistanceToNextTarget;
                            _robot.Position_Orientation = robots.Position_Orientation;
                            _robot.Position_X = robots.Position_X;
                            _robot.Position_Y = robots.Position_Y;
                            _robot.Product = robots.Product;
                            _robot.Door = robots.Door;
                            _robot.PositionZoneName = robots.PositionZoneName;
                            _robot.ErrorsJson = robots.ErrorsJson;
                            _robot.RobotModel = robots.RobotModel;
                            _robot.RobotAlias = robots.RobotAlias;
                        }
                    }
                    return _robots;
                }
            }
        }


        public List<Robot> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<Robot>("SELECT * FROM Robots").ToList();

                }
            }
        }
        public IList<Robot> GetAll() => _robots;
        public List<Robot> Find(Func<Robot, bool> predicate)
        {
            lock (this)
            {
                return _robots.Where(predicate).ToList();
            }
        }


        public void Remove(Robot model)
        {
            _robots.Remove(model);

            using (var con = new SqlConnection(connectionString))
            {
                con.Execute("DELETE FROM Robots WHERE Id=@id",
                    param: new { id = model.Id });
                logger.Info($"Robot Remove: {model}");
            }
        }

        public void Update(Robot model)
        {
            if (!model.DataChanged) return;

            using (var con = new SqlConnection(connectionString))
            {
                const string UPDATE_SQL = @"
                    UPDATE Robots 
                    SET 
                        JobId=@JobId,
                        ACSRobotGroup=@ACSRobotGroup,
                        ACSRobotActive=@ACSRobotActive,
                        Fleet_State=@Fleet_State,
                        Fleet_State_Text=@Fleet_State_Text,
                        RobotID=@RobotID,
                        RobotIp=@RobotIp,
                        RobotName=@RobotName,
                        RobotAlias=@RobotAlias,
                        StateID=@StateID,
                        StateText=@StateText,
                        MissionText=@MissionText,
                        MissionQueueID=@MissionQueueID,
                        BatteryPercent=@BatteryPercent,
                        MapID=@MapID,
                        DistanceToNextTarget=@DistanceToNextTarget,
                        Position_Orientation=@Position_Orientation,
                        Position_X=@Position_X,
                        Position_Y=@Position_Y,
                        ErrorsJson=@ErrorsJson,
                        RobotModel=@RobotModel,
                        Product=@Product,
                        Door=@Door,
                        PositionZoneName=@PositionZoneName
                    WHERE Id=@Id";

                con.Execute(UPDATE_SQL, param: model);
                logger.Info($"Robot Update: {model}");

                model.DataChanged = false;
            }
        }

        public void Delete()
        {
            lock (this)
            {
                _robots.Clear();
                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM Robots");
                }
            }
        }

    }
}
