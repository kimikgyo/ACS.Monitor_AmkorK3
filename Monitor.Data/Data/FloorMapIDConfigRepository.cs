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
    public class FloorMapIDConfigRepository
    {
        //private readonly static ILog logger = LogManager.GetLogger("User");

        private readonly IDbConnection db;
        private readonly string connectionString = null;

        private readonly List<FloorMapIdConfigModel> _floorMapIDConfigModel = new List<FloorMapIdConfigModel>(); // cache data

        private readonly static object lockObj = new object();

        public FloorMapIDConfigRepository(string connectionString)
        {
            this.connectionString = connectionString;
            DBLoad();
        }

        //DB 추가하기
        public FloorMapIdConfigModel Add(FloorMapIdConfigModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO FloorMapIDConfigs
                                ([FloorIndex]
                                ,[FloorName]
                                ,[MapID]
                                ,[MapImageData]
                                ,[DisplayFlag])
                           VALUES
                                (@FloorIndex
                                ,@FloorName
                                ,@MapID
                                ,@MapImageData
                                ,@DisplayFlag);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                return model;
            }
        }

        //DB찾기
        public List<FloorMapIdConfigModel> Find(Func<FloorMapIdConfigModel, bool> predicate)
        {
            lock (this)
            {
                return _floorMapIDConfigModel.Where(predicate).ToList();
            }
        }

        //public List<FloorMapIdConfigModel> GetDisplayFlagtrueData()
        //{
        //    lock (this)
        //    {
        //        using (var con = new SqlConnection(connectionString))
        //        {
        //            return con.Query<FloorMapIdConfigModel>("SELECT * FROM FloorMapIDConfigs WHERE DisplayFlag=1").ToList();

        //        }
        //    }
        //}

        public IList<FloorMapIdConfigModel> GetAll()
        {
            lock (lockObj)
            {
                return _floorMapIDConfigModel;
            }
        }

        public List<FloorMapIdConfigModel> DBLoad()
        {
            lock (lockObj)
            {
                _floorMapIDConfigModel.Clear();
                using (var con = new SqlConnection(connectionString))
                {
                    foreach (var floorMapIDConfigs in con.Query<FloorMapIdConfigModel>("SELECT * FROM FloorMapIDConfigs WHERE DisplayFlag=1"))
                    {
                        _floorMapIDConfigModel.Add(floorMapIDConfigs);
                    }
                    return _floorMapIDConfigModel;
                }
            }
        }

        public List<FloorMapIdConfigModel> Update()
        {
            lock (lockObj)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    foreach (var floorMapIDConfigs in con.Query<FloorMapIdConfigModel>("SELECT * FROM FloorMapIDConfigs WHERE DisplayFlag=1"))
                    {
                       var Updata = _floorMapIDConfigModel.FirstOrDefault(x=>x.Id == floorMapIDConfigs.Id);
                        if(Updata!=null)
                        {
                            Updata.Id = floorMapIDConfigs.Id;
                            Updata.FloorIndex = floorMapIDConfigs.FloorIndex;
                            Updata.FloorName = floorMapIDConfigs.FloorName;
                            Updata.MapID = floorMapIDConfigs.MapID;
                            Updata.MapImageData = floorMapIDConfigs.MapImageData;
                        }
                    }
                    return _floorMapIDConfigModel;
                }
            }
        }

        public List<FloorMapIdConfigModel> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<FloorMapIdConfigModel>("SELECT * FROM FloorMapIDConfigs WHERE DisplayFlag=1").ToList();
                }
            }
        }
        //DB업데이트
        public void Update(FloorMapIdConfigModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE FloorMapIDConfigs 
                    SET 
                        FloorIndex=@FloorIndex, 
                        FloorName=@FloorName, 
                        MapID=@MapID, 
                        MapImageData=@MapImageData, 
                        DisplayFlag=@DisplayFlag
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);

                }
            }
        }

        //DB삭제
        public void Remove(FloorMapIdConfigModel model)
        {
            lock (this)
            {
                _floorMapIDConfigModel.Remove(model);

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM FloorMapIDConfigs WHERE Name LIKE @Name",
                        param: new { id = model.Id });
                }
            }
        }
    }
}
