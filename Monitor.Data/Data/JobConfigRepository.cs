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
    public class JobConfigRepository
    {
        private readonly IDbConnection db;
        private readonly string connectionString = null;
        private readonly List<JobConfigModel> _jobConfigs = new List<JobConfigModel>(); // cached data


        public JobConfigRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        private void Load()
        {
            _jobConfigs.Clear();
            using (var con = new SqlConnection(connectionString))
            {
                foreach (var missionConfigs in con.Query<JobConfigModel>("SELECT * FROM JobConfigs WHERE DisplayFlag=1"))
                    _jobConfigs.Add(missionConfigs);
            }
        }

        public JobConfigModel Add(JobConfigModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                _jobConfigs.Add(model);

                const string INSERT_SQL = @"
                    INSERT INTO JobConfigs
                               ([ACSMissionGroup]
                               ,[CallName]
                               ,[JobConfigUse]
                               ,[JobMissionName1]
                               ,[JobMissionName2]
                               ,[JobMissionName3]
                               ,[JobMissionName4]
                               ,[JobMissionName5]
                               ,[ExecuteBattery]
                               ,[jobCallSignal]
                               ,[jobCancelSignal]
                               ,[POSjobCallSignal_Reg32]
                               ,[POSjobCallSignal_Reg33]
                               ,[ProductActive]
                               ,[ProductValue]
                               ,[ElevatorModeActive]
                               ,[ElevatorModeValue]
                               ,[TransportCountActive]
                               ,[DisplayFlag]
                               ,[MissionName]
                               ,[ErrorMissionName])
                           VALUES
                               (@ACSMissionGroup
                               ,@CallName
                               ,@JobConfigUse
                               ,@JobMissionName1
                               ,@JobMissionName2
                               ,@JobMissionName3
                               ,@JobMissionName4
                               ,@JobMissionName5
                               ,@ExecuteBattery
                               ,@jobCallSignal
                               ,@jobCancelSignal
                               ,@POSjobCallSignal_Reg32
                               ,@POSjobCallSignal_Reg33
                               ,@ProductActive
                               ,@ProductValue
                               ,@ElevatorModeActive
                               ,@ElevatorModeValue
                               ,@TransportCountActive
                               ,@DisplayFlag
                               ,@MissionName
                               ,@ErrorMissionName);
                    SELECT Cast(SCOPE_IDENTITY() As Int);";

                int id = con.ExecuteScalar<int>(INSERT_SQL, param: model);
                model.Id = id;
                return model;
            }
        }

        public List<JobConfigModel> GetAll()
        {
            return _jobConfigs.ToList();
        }

        public List<JobConfigModel> DBGetAll()
        {
            lock (this)
            {
                using (var con = new SqlConnection(connectionString))
                {
                    return con.Query<JobConfigModel>("SELECT * FROM JobConfigs WHERE DisplayFlag=1").ToList();

                }
            }
        }

        public JobConfigModel GetById(int id)
        {
            return _jobConfigs.SingleOrDefault(x => x.Id == id);
        }

        public JobConfigModel GetByCallName(string aCSMissionName)
        {
            return _jobConfigs.FirstOrDefault(x => x.CallName == aCSMissionName);
        }

        public void Remove(JobConfigModel model)
        {
            _jobConfigs.Remove(model);

            using (var con = new SqlConnection(connectionString))
            {
                con.Execute("DELETE FROM JobConfigs WHERE Id=@id AND DisplayFlag=1",
                    param: new { id = model.Id });
            }
        }

        public void Update(JobConfigModel model)
        {
            using (var con = new SqlConnection(connectionString))
            {
                const string query = @"
                    UPDATE JobConfigs 
                    SET 
                       ACSMissionGroup=@ACSMissionGroup,
                       CallName=@CallName, 
                       JobConfigUse=@JobConfigUse, 
                       JobMissionName1=@JobMissionName1, 
                       JobMissionName2=@JobMissionName2, 
                       JobMissionName3=@JobMissionName3, 
                       JobMissionName4=@JobMissionName4, 
                       JobMissionName5=@JobMissionName5,
                       ExecuteBattery=@ExecuteBattery,
                       jobCallSignal=@jobCallSignal,
                       jobCancelSignal=@jobCancelSignal,
                       POSjobCallSignal_Reg32=@POSjobCallSignal_Reg32,
                       POSjobCallSignal_Reg33=@POSjobCallSignal_Reg33,
                       ProductValue=@ProductValue,
                       ProductActive=@ProductActive,
                       ElevatorModeValue=@ElevatorModeValue,
                       ElevatorModeActive=@ElevatorModeActive,
                       TransportCountActive=@TransportCountActive,
                       DisplayFlag=@DisplayFlag, 
                       MissionName=@MissionName, 
                       ErrorMissionName=@ErrorMissionName
                    WHERE Id=@Id";

                con.Execute(query, param: model);
            }
        }
        public List<JobConfigModel> Find(Func<JobConfigModel, bool> predicate)
        {
            lock (this)
            {
                return _jobConfigs.Where(predicate).ToList();
            }
        }
    }
}
