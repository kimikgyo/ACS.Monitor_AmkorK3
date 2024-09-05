using System.Collections.Generic;
using System.Text;

namespace Monitor.Map
{
    public class FleetMap
    {
        public string Name;
        public string Guid;
        public double OriginX;
        public double OriginY;
        public double OriginTheta;
        public double Resolution;
        public System.Drawing.Image Image = null;
        public List<FleetPosition> Positions = new List<FleetPosition>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("name         = {0}\n", Name);
            sb.AppendFormat("guid         = {0}\n", Guid);
            sb.AppendFormat("origin_x     = {0}\n", OriginX);
            sb.AppendFormat("origin_y     = {0}\n", OriginY);
            sb.AppendFormat("origin_theta = {0}\n", OriginTheta);
            sb.AppendFormat("positions    = {0}\n", Positions.Count);
            return sb.ToString();
        }
    }

    public class FleetPosition
    {
        public string Name;
        public string Guid;
        public string TypeID;
        public string MapID;
        public double PosX;
        public double PosY;
        public double Orientation;

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("\ttype_id    = {0}\n", TypeID);
            sb.AppendFormat("\tpos_x      = {0}\n", PosX);
            sb.AppendFormat("\tpos_y      = {0}\n", PosY);
            sb.AppendFormat("\tOrientation= {0}\n", Orientation);
            sb.AppendLine();
            return sb.ToString();
        }
    }

    public class FleetRobot
    {
        public int RobotID;
        public string RobotName;                       //MiR의 Model Name 불러오는 변수
        public string MapID;
        public double PosX;                            //MiR의 Position X Value 불러오는 변수
        public double PosY;                            //MiR의 Position Y Value 불러오는 변수
        public double Orientation;                     //MiR의 Position R Value 불러오는 변수
        public double DistanceToTarget;                //MiR의 다음 타겟까지의 거리 불러오는 변수
        public double BatteryPercent;                  //MiR의 Battery Percent(Text) 불러오는 변수
        public string MissionQueueID;                  //MiR의 현재 Mission Queue ID 불러오는 변수
        public string MissionText;                     //MiR의 Mission Text 불러오는 변수
        public string StateID;                         //MiR의 상태(ID) 불러오는 변수
        public string StateText;                       //MiR의 상태(Text) 불러오는 변수
        //public string ModeID;                          //MiR의 상태(ID) 불러오는 변수
        //public string ModeText;                        //MiR의 상태(Text) 불러오는 변수
        public List<string> FootPrints;
        public string IP;

        public string FleetState;
        public string FleetStateText;

        public string RobotAlias;               // amkor k5 로봇 네임 대신 닉네임으로 표시하기 위해 추가함


        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("robot_id               : {0}\n", RobotID);
            sb.AppendFormat("robot_name             : {0}\n", RobotName);
            sb.AppendFormat("map_id                 : {0}\n", MapID);
            sb.AppendFormat("position_x             : {0}\n", PosX);
            sb.AppendFormat("position_y             : {0}\n", PosY);
            sb.AppendFormat("position_orientation   : {0}\n", Orientation);
            return sb.ToString();
        }
    }
}