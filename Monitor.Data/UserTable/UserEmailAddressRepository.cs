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
    public class UserEmailAddressRepository
    {
        private readonly IDbConnection db;
        private readonly string connectionString = null;

        private readonly List<UserEmailAddressModel> _userEmailAddress = new List<UserEmailAddressModel>(); // cache data


        public UserEmailAddressRepository(string connectionString)
        {
            this.connectionString = connectionString;
           
        }

        private void Load()
        {
            _userEmailAddress.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var userEmailAddress in con.Query<UserEmailAddressModel>("SELECT * FROM UserEmailAddress WHERE DisplayFlag=1"))
                {

                    _userEmailAddress.Add(userEmailAddress);
                }
            }
        }
        //DB 추가하기
        public UserEmailAddressModel Add(UserEmailAddressModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO UserEmailAddress
                                ([EmailUse]
                                ,[UserEmailAddress]
                                ,[DisplayFlag])
                           VALUES
                                (@EmailUse
                                ,@UserEmailAddress
                                ,@DisplayFlag);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                //logger.Info($"PositionAreaConfig Add   : {model}");
                return model;
            }
        }

        //DB찾기
        public List<UserEmailAddressModel> Find(Func<UserEmailAddressModel, bool> predicate)
        {
            lock (this)
            {
                return _userEmailAddress.Where(predicate).ToList();
            }
        }

      
        public IList<UserEmailAddressModel> GetAll() => _userEmailAddress;


        public List<UserEmailAddressModel> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<UserEmailAddressModel>("SELECT * FROM UserEmailAddress WHERE DisplayFlag=1").ToList();

                }
            }
        }
        //DB업데이트
        public void Update(UserEmailAddressModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE UserEmailAddress
                    SET 
                        EmailUse=@EmailUse, 
                        UserEmailAddress=@UserEmailAddress, 
                        DisplayFlag=@DisplayFlag
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);
                    //logger.Info($"PositionAreaConfig Update: {model}");
                }
            }
        }


        //DB삭제
        public void Remove(UserEmailAddressModel model)
        {
            lock (this)
            {
                _userEmailAddress.Remove(model);

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM UserEmailAddress WHERE Id=@id",
                        param: new { id = model.Id });
                    //logger.Info($"PositionAreaConfig Remove: {model}");
                }
            }
        }
    }
}
