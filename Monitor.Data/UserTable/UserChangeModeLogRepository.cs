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
    public class UserChangeModeLogRepository
    {
        private readonly IDbConnection db;
        private readonly string connectionString = null;
        public UserChangeModeLogRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(UserChangeModeLog userChangeModeLog )
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
               INSERT INTO UserChangeModeLog
                            ([UserNumber]
                            ,[UserName]
                            ,[ChangeModeLog]
                            ,[CreateTime])
                       VALUES 
                            (@UserNumber
                            ,@UserName
                            ,@ChangeModeLog
                            ,@CreateTime);
                     SELECT Cast(SCOPE_IDENTITY() As Int);";

                int id = con.ExecuteScalar<int>(INSERT_SQL, param: userChangeModeLog);
                userChangeModeLog.Id = id;
                //return jobLog;
            }
        }

        //Query 적용 부분을 그대로 Return 하기위해 IEnumerable<dynamic>를 사용
        public IEnumerable<dynamic> GetUserChangeModeLogBindingSource(string SELECT_SQL)
        {
            using (var con = new SqlConnection(connectionString))
            {
                return con.Query(SELECT_SQL).ToList();
            }

        }

        public IEnumerable<dynamic> GetUserChangeModeLogBindingSource_AnynymousType(string SELECT_SQL, object queryParams)
        {
            using (var con = new SqlConnection(connectionString))
            {
                return con.Query(SELECT_SQL, queryParams).ToList();
            }
        }

        public IEnumerable<dynamic> GetUserChangeModeLogBindingSource_Count_From_To(string SELECT_SQL, object queryParams)
        {
            using (var con = new SqlConnection(connectionString))
            {
                return con.Query(SELECT_SQL, queryParams).ToList();
            }
        }

    }
}
