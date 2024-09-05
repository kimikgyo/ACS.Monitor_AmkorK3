using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Common
{
   
    public class ElevatorStateModel
    {
        //========================================================= Elevator Mission 정보
        public int Id { get; set; }
        public string RobotAlias { get; set; }
        public string RobotName { get; set; }                       //Elevator 사용중인 Robot 이름
        public string ElevatorState { get; set; }                   //Elevator 상태
        public string MiRStateElevator { get; set; }                //Elevator 사용중인 Robot 상태
        public string ElevatorMissionName { get; set; }             //Robot 목적지 확인하기 위함
        
        public string StartFloor { get; set; }                      //Robot 출발 층

        public string EndFloor { get; set; }                        //Robot 목적지 층

        //public string ElevatorMode { get; set; }

    }
}
