using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monitor.Map
{
    public partial class FleetMapProcessor
    {
        private readonly string sFleet_ResponseTime = "30";
        private readonly ILog logger;
        private readonly string uriString;
        private readonly string MapName;

        public FleetMapProcessor(ILog logger, string uriString, string name = "")
        {
            this.logger = logger;
            this.uriString = uriString;
            this.MapName = name; // DateTime.Now.Ticks.ToString();
        }


        public List<string> GetMapIDs()
        {
            List<FleetMap> maps = _Thread_Fleet_ReST_Send("GET_MAPS", "", "", "") as List<FleetMap>;
            var ids = new List<string>();

            foreach (var x in maps)
            {
                ids.Add(x.Guid);
            }

            return ids;
        }


        public List<string> GetMapNames()
        {
            List<FleetMap> maps = _Thread_Fleet_ReST_Send("GET_MAPS", "", "", "") as List<FleetMap>;
            var ids = new List<string>();

            foreach (var x in maps)
            {
                ids.Add(x.Name);
            }

            return ids;
        }


        public FleetMap GetMap(string map_id, bool readPositions = true)
        {

            // get map
            var map = _Thread_Fleet_ReST_Send("GET_MAPS_ID", map_id, "", "") as FleetMap;
            if (map != null)
            {
                // get positions
                if (readPositions) map.Positions = GetPositions(map_id);

                // debug print
                var sb = new StringBuilder();
                sb.AppendFormat("name         = {0}\n", map.Name);
                sb.AppendFormat("guid         = {0}\n", map.Guid);
                sb.AppendFormat("origin_x     = {0}\n", map.OriginX);
                sb.AppendFormat("origin_y     = {0}\n", map.OriginY);
                sb.AppendFormat("origin_theta = {0}\n", map.OriginTheta);
                sb.AppendFormat("resolution   = {0}\n", map.Resolution);
                sb.AppendFormat("image_size   = {0}\n", map.Image != null ? $"( {map.Image.Width},{map.Image.Height} )" : "");
                sb.AppendFormat("positions    = {0}\n", map.Positions.Count);
                map.Positions.ForEach(p =>
                    {
                        sb.AppendFormat("\ttype_id    = {0}\n", p.TypeID);
                        sb.AppendFormat("\tname       = {0}\n", p.Name);
                        sb.AppendFormat("\tpos_x      = {0}\n", p.PosX);
                        sb.AppendFormat("\tpos_y      = {0}\n", p.PosY);
                        sb.AppendFormat("\tOrientation= {0}\n", p.Orientation);
                        sb.AppendLine();
                    });

                Console.WriteLine(">>>>> GET_MAPS_ID/{0}", map.Guid);
                Console.WriteLine(sb.ToString());
            }
            return map;
        }


        public List<FleetPosition> GetPositions(string map_id)
        {
            var newPositions = new List<FleetPosition>();

            // get position list
            var tempPositions = _Thread_Fleet_ReST_Send("GET_MAPS_ID_POSITIONS", map_id, "", "") as List<FleetPosition>;
            if (tempPositions != null)
            {
                // get position
                foreach (string pos_id in tempPositions.Select(p => p.Guid))
                {
                    var tempPos = GetPosition(pos_id);
                    if (tempPos != null)
                    {
                        newPositions.Add(tempPos);
                    }
                }
            }
            return newPositions;
        }


        public FleetPosition GetPosition(string pos_id)
        {
            var pos = _Thread_Fleet_ReST_Send("GET_POSITIONS_ID", pos_id, "", "") as FleetPosition;
            return pos;
        }


        public List<int> GetRobotIdList()
        {
            var robotIdList = _Thread_Fleet_ReST_Send("GET_ROBOTS", "", "", "") as List<int>;
            return robotIdList;
        }


        public List<FleetRobot> GetRobots(IList<int> robotIdList)
        {
            var robots = new List<FleetRobot>();
            foreach (var robot_id in robotIdList)
            {
                var robot = GetRobot(robot_id);
                if (robot != null)
                {
                    robots.Add(robot);
                }
            }
            return robots;
        }


        public FleetRobot GetRobot(int robot_id)
        {
            var robot = _Thread_Fleet_ReST_Send("GET_ROBOT_ID", robot_id.ToString(), "", "") as FleetRobot;
            if (robot != null)
            {
                // debug print
                var sb = new StringBuilder();
                sb.AppendFormat(">>>>> GET ROBOT/{0}           ", robot_id);
                sb.AppendFormat("robot_id               : {0}\n", robot.RobotID);
                sb.AppendFormat("robot_name             : {0}\n", robot.RobotName);
                sb.AppendFormat("map_id                 : {0}\n", robot.MapID);
                sb.AppendFormat("position_x             : {0}\n", robot.PosX);
                sb.AppendFormat("position_y             : {0}\n", robot.PosY);
                sb.AppendFormat("position_orientation   : {0}\n", robot.Orientation);
                sb.AppendFormat("battery_percent        : {0:0}%\n", robot.BatteryPercent);
                sb.AppendFormat("distance_to_target     : {0}\n", robot.DistanceToTarget);
                sb.AppendFormat("state_text             : {0}\n", robot.StateText);
                //sb.AppendFormat("mode_text              : {0}\n", robot.ModeText);
                sb.AppendFormat("mission_queue_id       : {0}\n", robot.MissionQueueID);
                sb.AppendFormat("mission_text           : {0}\n", robot.MissionText);
                //sb.AppendFormat("footprint              : {0}\n", string.Join(",", robot.FootPrints));

                sb.AppendFormat("fleet_state            : {0}\n", robot.FleetState);
                sb.AppendFormat("fleet_state_text       : {0}\n", robot.FleetStateText);

                sb.AppendLine();
            }
            return robot;
        }

    }
}
