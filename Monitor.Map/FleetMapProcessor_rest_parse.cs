using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace Monitor.Map
{
    public partial class FleetMapProcessor
    {
        #region Fleet REST parsing

        // GET_MAPS
        private List<FleetMap> subFuncFleet_ReST_ParsingMaps(string json)
        {
            try
            {
                JArray array = JArray.Parse(json);

                var maps = new List<FleetMap>();

                foreach (JToken map in array)
                {
                    var newMap = new FleetMap()
                    {
                        Name = map["name"].Value<string>(),
                        Guid = map["guid"].Value<string>(),
                    };

                    maps.Add(newMap);
                }

                return maps;
            }
            catch (Exception e2) { logger.Info($"{System.Reflection.MethodBase.GetCurrentMethod().Name} Load Fail=" + e2); }
            return null;
        }

        // GET_MAPS_ID
        private FleetMap subFuncFleet_ReST_ParsingMapsID(string json)
        {
            try
            {
                JObject array = JObject.Parse(json);

                // parse map
                var mapEncodedString = array["map"].Value<string>();

                // create map image
                System.Drawing.Image mapImage = null;
                using (var ms = new MemoryStream())
                {
                    byte[] mapDecodedBytes = Convert.FromBase64String(mapEncodedString);
                    ms.Write(mapDecodedBytes, 0, mapDecodedBytes.Length);
                    mapImage = System.Drawing.Image.FromStream(ms);
                }

                // create map object
                var newMap = new FleetMap
                {
                    Image = mapImage,
                    Name = array["name"].Value<string>(),
                    Guid = array["guid"].Value<string>(),
                    OriginX = array["origin_x"].Value<double>(),
                    OriginY = array["origin_y"].Value<double>(),
                    OriginTheta = array["origin_theta"].Value<double>(),
                    Resolution = array["resolution"].Value<double>(),
                };
                return newMap;
            }
            catch (Exception e2) { logger.Info($"{System.Reflection.MethodBase.GetCurrentMethod().Name} Load Fail=" + e2); }
            return null;
        }

        // GET_MAPS_ID_POSITIONS
        private List<FleetPosition> subFuncFleet_ReST_ParsingPositions(string json)
        {
            try
            {
                JArray array = JArray.Parse(json);

                var positions = new List<FleetPosition>();

                foreach (JToken pos in array)
                {
                    var newPosition = new FleetPosition()
                    {
                        Name = pos["name"].Value<string>(),
                        Guid = pos["guid"].Value<string>(),
                        TypeID = pos["type_id"].Value<string>(),
                    };

                    positions.Add(newPosition);
                }

                return positions;
            }
            catch (Exception e2) { logger.Info($"{System.Reflection.MethodBase.GetCurrentMethod().Name} Load Fail=" + e2); }
            return null;
        }

        // GET_POSITIONS_ID
        private FleetPosition subFuncFleet_ReST_ParsingPositionsID(string json)
        {
            try
            {
                JObject array = JObject.Parse(json);

                var newPosition = new FleetPosition
                {
                    Name = array["name"].Value<string>(),
                    Guid = array["guid"].Value<string>(),
                    TypeID = array["type_id"].Value<string>(),
                    MapID = array["map_id"].Value<string>(),
                    PosX = array["pos_x"].Value<double>(),
                    PosY = array["pos_y"].Value<double>(),
                    Orientation = array["orientation"].Value<double>(),
                };
                return newPosition;
            }
            catch (Exception e2) { logger.Info($"{System.Reflection.MethodBase.GetCurrentMethod().Name} Load Fail=" + e2); }
            return null;
        }

        // GET_ROBOTS
        private List<int> subFuncFleet_ReST_ParsingRobots(string json)
        {
            try
            {
                JArray array = JArray.Parse(json);

                var robotIDs = new List<int>();

                foreach (JToken item in array)
                {
                    int RobotID = item["id"].Value<int>();
                    robotIDs.Add(RobotID);
                }
                return robotIDs;
            }
            catch (Exception e2) { logger.Info($"{System.Reflection.MethodBase.GetCurrentMethod().Name} Load Fail=" + e2); }
            return null;
        }

        // GET_ROBOT_ID
        private FleetRobot subFuncFleet_ReST_ParsingRobotsID(string json)
        {
            try
            {
                JObject array = JObject.Parse(json);

                // status
                JToken token = array["status"];
                FleetRobot newRobot = _Thread_Fleet_ReST_ParsingRobotStatus(token.ToString());
                if (newRobot != null)
                {
                    newRobot.RobotID = array["id"].Value<int>();
                    newRobot.IP = array["ip"].Value<string>();

                    newRobot.FleetState = array["fleet_state"].Value<string>();
                    newRobot.FleetStateText = array["fleet_state_text"].Value<string>();
                }
                return newRobot;
            }
            catch (Exception e2) { logger.Info($"{System.Reflection.MethodBase.GetCurrentMethod().Name} Load Fail=" + e2); }
            return null;
        }

        // GET_ROBOT_ID (status)
        private FleetRobot _Thread_Fleet_ReST_ParsingRobotStatus(string json)
        {
            try
            {
                JObject status = JObject.Parse(json);

                //var RobotID = array["id"].Value<int>(); // id 는 "status" 객체에 속하지 않는다
                var RobotName = status["robot_name"].Value<string>();
                var MapID = status["map_id"].Value<string>();
                var Position_X = status["position"]["x"].Value<double>();
                var Position_Y = status["position"]["y"].Value<double>();
                var Position_Orientation = status["position"]["orientation"].Value<double>();
                var DistanceToTarget = status["distance_to_next_target"].Value<double>();
                var BatteryPercent = status["battery_percentage"].Value<double>();
                var MissionQueueID = status["mission_queue_id"].Value<string>();
                var MissionText = status["mission_text"].Value<string>();
                var StateID = status["state_id"].Value<string>();
                var StateText = status["state_text"].Value<string>();
                //var ModeID = status["mode_id"].Value<string>();
                //var ModeText = status["mode_text"].Value<string>();

                //JArray FootPrint = JArray.Parse(status["footprint"].ToString());
                //List<string> FootPrints = new List<string>();
                //for (int i = 0; i < FootPrint.Count; i++)
                //{
                //    JArray FootPrint1 = JArray.Parse(FootPrint[i].ToString());
                //    string x = FootPrint1[0].Value<string>();
                //    string y = FootPrint1[1].Value<string>();
                //    string point = $"({x},{y})";
                //    FootPrints.Add(point);
                //}

                var newRobot = new FleetRobot()
                {
                    RobotName = RobotName,
                    MapID = MapID,
                    PosX = Position_X,
                    PosY = Position_Y,
                    Orientation = Position_Orientation,
                    DistanceToTarget = DistanceToTarget,
                    BatteryPercent = BatteryPercent,
                    MissionQueueID = MissionQueueID,
                    MissionText = MissionText,
                    StateID = StateID,
                    StateText = StateText,
                    //ModeID = ModeID,
                    //ModeText = ModeText,
                    //FootPrints = FootPrints,
                };
                return newRobot;
            }
            catch (Exception e2) { logger.Info($"{System.Reflection.MethodBase.GetCurrentMethod().Name} Load Fail=" + e2); }
            return null;
        }

        #endregion

    }
}
