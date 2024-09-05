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
    public class UserNumberRepositoy
    {
        private readonly IDbConnection db;
        private readonly string connectionString = null;

        private readonly List<UserNumberModel> _UserNumberModels = new List<UserNumberModel>(); // cache data

        public UserNumberRepositoy(string connectionString)
        {
            this.connectionString = connectionString;
           
        }

        private void Load()
        {
            _UserNumberModels.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var waitMissionConfig in con.Query<UserNumberModel>("SELECT * FROM UserNumber WHERE DisplayFlag=1"))
                {

                    _UserNumberModels.Add(waitMissionConfig);
                }
            }
        }

        //DB 추가하기
        public UserNumberModel Add(UserNumberModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO UserNumber
                                ([UserNumber]
                                ,[UserName]
                                ,[UserPassword]
                                ,[CallMissionAuthority]
                                ,[ElevatorAuthority]
                                ,[DisplayFlag])
                           VALUES
                                (@UserNumber
                                ,@UserName
                                ,@UserPassword
                                ,@CallMissionAuthority
                                ,@ElevatorAuthority
                                ,@DisplayFlag);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                //logger.Info($"PositionAreaConfig Add   : {model}");
                return model;
            }
        }

        //DB찾기
        public List<UserNumberModel> Find(Func<UserNumberModel, bool> predicate)
        {
            lock (this)
            {
                return _UserNumberModels.Where(predicate).ToList();
            }
        }

        public IList<UserNumberModel> GetAll() => _UserNumberModels;


        public List<UserNumberModel> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<UserNumberModel>("SELECT * FROM UserNumber  WHERE DisplayFlag=1").ToList();

                }
            }
        }

        //DB업데이트
        public void Update(UserNumberModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE UserNumber
                    SET 
                        UserNumber=@UserNumber, 
                        UserName=@UserName, 
                        UserPassword=@UserPassword, 
                        CallMissionAuthority=@CallMissionAuthority, 
                        ElevatorAuthority=@ElevatorAuthority, 
                        DisplayFlag=@DisplayFlag
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);
                    //logger.Info($"PositionAreaConfig Update: {model}");
                }
            }
        }


        //DB삭제
        public void Remove(UserNumberModel model)
        {
            lock (this)
            {
                _UserNumberModels.Remove(model);

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM UserNumber WHERE Id=@id",
                        param: new { id = model.Id });
                    //logger.Info($"PositionAreaConfig Remove: {model}");
                }
            }
        }
    }
}
