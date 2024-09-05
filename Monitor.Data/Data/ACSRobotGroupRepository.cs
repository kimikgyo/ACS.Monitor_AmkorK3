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
    public class ACSRobotGroupRepository
    {
        private readonly IDbConnection db;
        private readonly string connectionString = null;

        private readonly List<ACSRobotGroupConfigModel> _aCSRobotGroupModel = new List<ACSRobotGroupConfigModel>(); // cache data


        public ACSRobotGroupRepository(string connectionString)
        {
            this.connectionString = connectionString;
          
        }

        private void Load()
        {
            _aCSRobotGroupModel.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var aCSRobotGroupModel in con.Query<ACSRobotGroupConfigModel>("SELECT * FROM ACSGroupConfigs WHERE DisplayFlag=1"))
                {

                    _aCSRobotGroupModel.Add(aCSRobotGroupModel);
                }
            }
        }
        //DB 추가하기
        public ACSRobotGroupConfigModel Add(ACSRobotGroupConfigModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO ACSGroupConfigs
                                ([GroupUse]
                                ,[GroupName]
                                ,[DisplayFlag])
                           VALUES
                                (@GroupUse
                                ,@GroupName
                                ,@DisplayFlag);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                //logger.Info($"PositionAreaConfig Add   : {model}");
                return model;
            }
        }

        //DB찾기
        public List<ACSRobotGroupConfigModel> Find(Func<ACSRobotGroupConfigModel, bool> predicate)
        {
            lock (this)
            {
                return _aCSRobotGroupModel.Where(predicate).ToList();
            }
        }
        public IList<ACSRobotGroupConfigModel> GetAll() => _aCSRobotGroupModel;


        public List<ACSRobotGroupConfigModel> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<ACSRobotGroupConfigModel>("SELECT * FROM ACSGroupConfigs WHERE DisplayFlag=1").ToList();

                }
            }
        }


        //DB업데이트
        public void Update(ACSRobotGroupConfigModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE ACSGroupConfigs 
                    SET 
                        GroupUse=@GroupUse, 
                        GroupName=@GroupName, 
                        DisplayFlag=@DisplayFlag
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);
                    //logger.Info($"PositionAreaConfig Update: {model}");
                }
            }
        }


        //DB삭제
        public void Remove(ACSRobotGroupConfigModel model)
        {
            lock (this)
            {
                _aCSRobotGroupModel.Remove(model);

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM ACSGroupConfigs WHERE Id=@id",
                        param: new { id = model.Id });
                    //logger.Info($"PositionAreaConfig Remove: {model}");
                }
            }
        }
    }
}


