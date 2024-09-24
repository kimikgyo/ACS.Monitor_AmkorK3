using Monitor.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACS.Monitor
{
    public partial class GetDataControl
    {
        private readonly MainForm main;
        private readonly IUnitOfWork uow;
        bool Init = false;
        public GetDataControl(MainForm main, IUnitOfWork uow)
        {
            this.main = main;
            this.uow = uow;
        }
        public void Start()
        {
            Task.Run(() => Loop());
        }
        protected async Task Loop()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            while (true)
            {
                try
                {
                    ConfigDataInit();
                    DBGetAll();
                    await Task.Delay(1000);
                }

                catch (Exception ex)
                {
                    Console.WriteLine($"{ex.Message} / {ex.StackTrace} / {ex.InnerException}");
                }
            }
        }

        private void ConfigDataInit()
        {
           //if(ConfigData.Robots != null) ConfigData.Robots.Clear();
           //if(ConfigData.Jobs != null) ConfigData.Jobs.Clear();
           //if(ConfigData.Missions != null) ConfigData.Missions.Clear();
           //if(ConfigData.FloorMapIdConfigs != null) ConfigData.FloorMapIdConfigs.Clear();
           //if(ConfigData.PositionWaitTimes != null) ConfigData.PositionWaitTimes.Clear();
           //if(ConfigData.ElevatorStates != null) ConfigData.ElevatorStates.Clear();
           //if(ConfigData.ElevatorInfos != null) ConfigData.ElevatorInfos.Clear();
           //if(ConfigData.CustomMaps != null) ConfigData.CustomMaps.Clear();
           //if(ConfigData.FleetPositions != null) ConfigData.FleetPositions.Clear();
           //if(ConfigData.MissionsSpecifics != null) ConfigData.MissionsSpecifics.Clear();
           //if(ConfigData.JobConfigs != null) ConfigData.JobConfigs.Clear();
        }
        private void DBGetAll()
        {
            ConfigData.Robots = uow.Robots.DBGetAll();
            ConfigData.Jobs = uow.Jobs.DBGetAll();
            ConfigData.Missions = uow.Missions.DBGetAll();
            ConfigData.PositionWaitTimes = uow.PositionWaitTimes.DBGetAll();
            ConfigData.ElevatorStates = uow.ElevatorStates.DBGetAll();
            ConfigData.ElevatorInfos = uow.ElevatorInfos.DBGetAll();
            ConfigData.CustomMaps = uow.CustomMaps.DBGetAll();
            ConfigData.FleetPositions = uow.FleetPositions.DBGetAll();
            ConfigData.MissionsSpecifics = uow.MissionsSpecifics.DBGetAll();
            ConfigData.JobConfigs = uow.JobConfigs.DBGetAll();
            ConfigData.PositionAreaConfigs = uow.PositionAreaConfigs.DBGetAll();
            if (!Init)
            { ConfigData.FloorMapIdConfigs = uow.FloorMapIDConfigs.DBGetAll(); Init = true; }
        }
    }
}
