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
    public class ProductNameInfoRepository
    {
        //private readonly static ILog logger = LogManager.GetLogger("User");

        private readonly IDbConnection db;
        private readonly string connectionString = null;

        private readonly List<ProductNameInfoModel> _productNameInfoModel = new List<ProductNameInfoModel>(); // cache data


        public ProductNameInfoRepository(string connectionString)
        {
            this.connectionString = connectionString;
         
        }
        private void Load()
        {
            _productNameInfoModel.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var productNameInfo in con.Query<ProductNameInfoModel>("SELECT * FROM ProductNameInfoModel WHERE DisplayFlag=1"))
                {

                    _productNameInfoModel.Add(productNameInfo);
                }
            }
        }
        //DB 추가하기
        public ProductNameInfoModel Add(ProductNameInfoModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string INSERT_SQL = @"
                    INSERT INTO ProductNameInfoModel
                                ([RobotGroup]
                                ,[Regiser22Vlaue]
                                ,[RegisterNo]
                                ,[RegisterValue]
                                ,[ProductName]
                                ,[DisplayFlag])
                           VALUES
                                (@RobotGroup
                                ,@Regiser22Vlaue
                                ,@RegisterNo
                                ,@RegisterValue
                                ,@ProductName
                                ,@DisplayFlag);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                model.Id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                //logger.Info($"PositionAreaConfig Add   : {model}");
                return model;
            }
        }

        //DB찾기
        public List<ProductNameInfoModel> Find(Func<ProductNameInfoModel, bool> predicate)
        {
            lock (this)
            {
                return _productNameInfoModel.Where(predicate).ToList();
            }
        }


        public IList<ProductNameInfoModel> GetAll() => _productNameInfoModel;
        public List<ProductNameInfoModel> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<ProductNameInfoModel>("SELECT * FROM ProductNameInfoModel WHERE DisplayFlag=1").ToList();

                }
            }
        }

        //DB업데이트
        public void Update(ProductNameInfoModel model)
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    const string UPDATE_SQL = @"
                    UPDATE ProductNameInfoModel 
                    SET 
                        RobotGroup=@RobotGroup, 
                        Regiser22Vlaue=@Regiser22Vlaue, 
                        RegisterNo=@RegisterNo, 
                        RegisterValue=@RegisterValue, 
                        ProductName=@ProductName, 
                        DisplayFlag=@DisplayFlag
                    WHERE Id=@Id";

                    con.Execute(UPDATE_SQL, param: model);

                }
            }
        }

        //DB삭제
        public void Remove(ProductNameInfoModel model)
        {
            lock (this)
            {
                _productNameInfoModel.Remove(model);

                using (var con = new SqlConnection(connectionString))
                {
                    con.Execute("DELETE FROM ProductNameInfoModel WHERE Name LIKE @Name",
                        param: new { id = model.Id });
                }
            }
        }
    }
}
