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
    public class ElevatorStateRepository
    {
        private readonly ILog ElevatorEventlogger = LogManager.GetLogger("ElevatorEvent");

        private readonly IDbConnection db;
        private readonly string connectionString;
        private readonly List<ElevatorStateModel> _elevatorStateModule = new List<ElevatorStateModel>(); // cache data


        public ElevatorStateRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //DB 불러오기
        public List<ElevatorStateModel> DBLoad()
        {
            _elevatorStateModule.Clear();


            using (var con = new SqlConnection(connectionString))
            {
                foreach (var skyNetModels in con.Query<ElevatorStateModel>("SELECT * FROM ElevatorState"))
                {
                    _elevatorStateModule.Add(skyNetModels);
                }
                return _elevatorStateModule.ToList();
            }
        }
        //DB 추가하기
        public ElevatorStateModel Add(ElevatorStateModel model)
        {
            _elevatorStateModule.Add(model);

            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO ElevatorState
                                ([RobotAlias]
                                ,[RobotName]
                                ,[MiRStateElevator]
                                ,[ElevatorState]
                                ,[ElevatorMissionName]
                                ,[StartFloor]
                                ,[EndFloor])
                           VALUES
                                (@RobotAlias
                                ,@RobotName
                                ,@MiRStateElevator
                                ,@ElevatorState
                                ,@ElevatorMissionName
                                ,@StartFloor
                                ,@EndFloor);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                ElevatorEventlogger.Info($"ElevatorState Robot Add   : {model}");
                return model;
            }
        }

        //DB찾기
        public List<ElevatorStateModel> Find(Func<ElevatorStateModel, bool> predicate)
        {
            lock (this)
            {
                return _elevatorStateModule.Where(predicate).ToList();
            }
        }

        public IList<ElevatorStateModel> GetAll() => _elevatorStateModule;


        //DB업데이트
        public void Update(ElevatorStateModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE ElevatorState 
                    SET 
                        RobotAlias=@RobotAlias,
                        RobotName=@RobotName, 
                        MiRStateElevator=@MiRStateElevator, 
                        ElevatorState=@ElevatorState,
                        ElevatorMissionName=@ElevatorMissionName,
                        StartFloor=@StartFloor,
                        EndFloor=@EndFloor
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);

                    ElevatorEventlogger.Info($"ElevatorState Update: {model}");

                }
            }
        }


        //DB삭제
        public void Remove(ElevatorStateModel model)
        {
            lock (this)
            {
                _elevatorStateModule.Remove(model);    // Skynet_Robot_DataSend Data 자체 제거한다

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM ElevatorState WHERE Id=@id",
                        param: new { id = model.Id });
                    ElevatorEventlogger.Info($"ElevatorState Remove: {model}");
                }
            }
        }
        public void Delete()
        {
            lock (this)
            {
                _elevatorStateModule.Clear();
                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM ElevatorState");
                }
            }
        }
    }
}
