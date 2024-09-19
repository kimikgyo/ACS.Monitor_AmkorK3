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
                    DBGetAll();
                    await Task.Delay(1000);
                }

                catch (Exception ex)
                {

                }
            }
        }
        private void DBGetAll()
        {
            //ConfigData는 UCMap에서 사용
            ConfigData.Robots = uow.Robots.ListUpdate();
            var Jobs = uow.Jobs.DBLoad();
            var Missions = uow.Missions.DBLoad();
            ConfigData.FloorMapIdConfigs = uow.FloorMapIDConfigs.ListUpdate();
            var PositionWaitTimes = uow.PositionWaitTimes.DBLoad();
            var ElevatorStates = uow.ElevatorStates.ListUpdate();
            var ElevatorInfo = uow.ElevatorInfos.ListUpdate();
            ConfigData.CustomMaps = uow.CustomMaps.ListUpdate();
            ConfigData.FleetPositions = uow.FleetPositions.ListUpdate();

        }
    }
}
