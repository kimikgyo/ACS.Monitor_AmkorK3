using Dapper;
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
    public class ACSChargerCountConfigRepository
    {
        private readonly IDbConnection db;
        private readonly string connectionString = null;

        private readonly List<ACSChargerCountConfigModel> _aCSChargerCountConfigModel = new List<ACSChargerCountConfigModel>(); // cache data


        public ACSChargerCountConfigRepository(string connectionString)
        {
            this.connectionString = connectionString;
        
        }
        private void Load()
        {
            _aCSChargerCountConfigModel.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var aCSChargerCountConfigModel in con.Query<ACSChargerCountConfigModel>("SELECT * FROM ACSChargerCountConfig WHERE DisplayFlag=1"))
                {

                    _aCSChargerCountConfigModel.Add(aCSChargerCountConfigModel);
                }
            }
        }
        //DB 추가하기
        public ACSChargerCountConfigModel Add(ACSChargerCountConfigModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO ACSChargerCountConfig
                                ([ChargerCountUse]
                                ,[RobotGroupName]
                                ,[FloorName]
                                ,[FloorMapId]
                                ,[ChargerGroupName]
                                ,[ChargerCount]
                                ,[ChargerCountStatus]
                                ,[DisplayFlag])
                           VALUES
                                (@ChargerCountUse
                                ,@RobotGroupName
                                ,@FloorName
                                ,@FloorMapId
                                ,@ChargerGroupName
                                ,@ChargerCount
                                ,@ChargerCountStatus
                                ,@DisplayFlag);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                //logger.Info($"PositionAreaConfig Add   : {model}");
                return model;
            }
        }

        //DB찾기
        public List<ACSChargerCountConfigModel> Find(Func<ACSChargerCountConfigModel, bool> predicate)
        {
            lock (this)
            {
                return _aCSChargerCountConfigModel.Where(predicate).ToList();
            }
        }
        public IList<ACSChargerCountConfigModel> GetAll() => _aCSChargerCountConfigModel;

        public List<ACSChargerCountConfigModel> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<ACSChargerCountConfigModel>("SELECT * FROM ACSChargerCountConfig WHERE DisplayFlag=1").ToList();
                }
            }
        }

        //DB업데이트
        public void Update(ACSChargerCountConfigModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE ACSChargerCountConfig 
                    SET 
                        ChargerCountUse=@ChargerCountUse, 
                        RobotGroupName=@RobotGroupName, 
                        FloorName=@FloorName,
                        FloorMapId=@FloorMapId,
                        ChargerGroupName=@ChargerGroupName,
                        ChargerCount=@ChargerCount,
                        ChargerCountStatus=@ChargerCountStatus,
                        DisplayFlag=@DisplayFlag
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);
                    //logger.Info($"PositionAreaConfig Update: {model}");
                }
            }
        }


        //DB삭제
        public void Remove(ACSChargerCountConfigModel model)
        {
            lock (this)
            {
                _aCSChargerCountConfigModel.Remove(model);

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM ACSChargerCountConfig WHERE Id=@id",
                        param: new { id = model.Id });
                    //logger.Info($"PositionAreaConfig Remove: {model}");
                }
            }
        }
    }
}
