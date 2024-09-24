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
    public class ElevatorInfoRepository
    {
        private readonly IDbConnection db;
        private readonly string connectionString = null;
        private readonly List<ElevatorInfoModel> _elevatorInfoModels = new List<ElevatorInfoModel>(); // cache data

        private readonly static object lockObj = new object();
        public ElevatorInfoRepository(string connectionString)
        {
            this.connectionString = connectionString;
            DBGetAll();
        }

        public List<ElevatorInfoModel> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<ElevatorInfoModel>("SELECT * FROM ElevatorInfo").ToList();
                }
            }
        }

        public List<ElevatorInfoModel> DBLoad()
        {
            lock (lockObj)
            {
                _elevatorInfoModels.Clear();
                using (var con = new SqlConnection(connectionString))
                {
                    foreach (var skyNetModels in con.Query<ElevatorInfoModel>("SELECT * FROM ElevatorInfo"))
                    {
                        _elevatorInfoModels.Add(skyNetModels);
                    }
                    return _elevatorInfoModels.ToList();
                }
            }
        }
        public List<ElevatorInfoModel> ListUpdate()
        {
            lock (lockObj)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    foreach (var ElevatorInfo in con.Query<ElevatorInfoModel>("SELECT * FROM ElevatorInfo "))
                    {
                        var Updata = _elevatorInfoModels.FirstOrDefault(x => x.Id == ElevatorInfo.Id);
                        if (Updata != null)
                        {
                            Updata.Id = ElevatorInfo.Id;
                            Updata.Location = ElevatorInfo.Location;
                            Updata.ACSMode = ElevatorInfo.ACSMode;
                            Updata.ElevatorMode = ElevatorInfo.ElevatorMode;
                            Updata.FloorIndex = ElevatorInfo.FloorIndex;
                            Updata.TransportMode = ElevatorInfo.TransportMode;
                            Updata.UserNumber = ElevatorInfo.UserNumber;
                        }
                      
                    }
                    return _elevatorInfoModels;
                }
            }
        }
        public IList<ElevatorInfoModel> GetAll() => _elevatorInfoModels;

        public ElevatorInfoModel Add(ElevatorInfoModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string INSERT_SQL = @"
                    INSERT INTO ElevatorInfo 
                                ([Location]
                                ,[ACSMode]
                                ,[ElevatorMode]
                                ,[floorIndex]
                                ,[TransportMode]
                                ,[UserNumber])
                            VALUES 
                                
                               (@Location
                               ,@ACSMode
                               ,@ElevatorMode
                               ,@floorIndex
                               ,@TransportMode
                               ,@UserNumber);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                    int id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                    model.Id = id;
                    return model;
                }
            }
        }

     
        //DB찾기
        public IEnumerable<ElevatorInfoModel> Find(Func<ElevatorInfoModel, bool> predicate)
        {
            lock (this)
            {

                using (var con = new SqlConnection(connectionString))
                {
                    var result = con.Query<ElevatorInfoModel>("SELECT * FROM ElevatorInfo");
                    return result.Where(predicate);
                }
            }
        }
        public ElevatorInfoModel GetById(int id)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<ElevatorInfoModel>("SELECT * FROM ElevatorInfo WHERE Id=@id",
                        param: new { id = id }).FirstOrDefault();
                }
            }
        }

        public void Remove(ElevatorInfoModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM ElevatorInfo WHERE Id=@id",
                        param: new { id = model.Id });
                }
            }
        }

        public void Update(ElevatorInfoModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string query = @"
                    UPDATE ElevatorInfo
                    SET 
                        Location      = @Location    ,
                        ACSMode       = @ACSMode     ,
                        ElevatorMode  = @ElevatorMode,
                        floorIndex    = @floorIndex  ,
                        TransportMode    = @TransportMode  ,
                        UserNumber = @UserNumber       
                      
                    WHERE Id=@Id";

                    con.Execute(query, param: model);
                }
            }
        }

        public void ACSModeUpdate(ElevatorInfoModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string query = @"
                    UPDATE ElevatorInfo
                    SET 
                        ACSMode  = @ACSMode,
                        UserNumber = @UserNumber
                    WHERE Location=@Location";

                    con.Execute(query, param: model);
                }
            }
        }
        public void ElevatorModeUpdate(ElevatorInfoModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string query = @"
                    UPDATE ElevatorInfo
                    SET 
                        ElevatorMode  = @ElevatorMode
                    WHERE Location=@Location";

                    con.Execute(query, param: model);
                }
            }
        }

    }
}
