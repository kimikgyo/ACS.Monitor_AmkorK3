using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Monitor.Map
{
    public class FleetMapRequest
    {
        public string Name { get; set; }
        public string Request { get; set; }
        public string Parameter { get; set; }
        public string RequestUriFull { get => string.Format(Request, Parameter); }
        public HttpResponseMessage Response { get; set; }
        public Func<string, object> ResponseParseFunction { get; set; }
        public object ResponseResult { get; set; }
    }

    public partial class FleetMapProcessor
    {
        private void Func_Test1()
        {
            string s1 = string.Format("{0}:{1}", "distributor", "distributor".ToSHA256());
            string s2 = s1.ToBase64Encode();
            string s3 = "Basic " + s2;
            Console.WriteLine(s3);
        }

        // test =====================================================

        public void TestFunc()
        {
            var sampleMessages = new FleetMapRequest[] {
                new FleetMapRequest()
                {
                    Name = "GET_MAPS",
                    Request = "api/v2.0.0/maps/",
                    Parameter = "",
                    ResponseParseFunction = subFuncFleet_ReST_ParsingMapsID,
                },
                new FleetMapRequest()
                {
                    Name = "GET_MAPS_ID",
                    Request = "api/v2.0.0/maps/{0}",
                    Parameter = "22cdbb33-b3bb-11ec-840c-94c691a734ac",
                    ResponseParseFunction = subFuncFleet_ReST_ParsingMapsID,
                },
                new FleetMapRequest()
                {
                    Name = "GET_MAPS_ID_POSITIONS",
                    Request = "api/v2.0.0/maps/{0}/positions/",
                    Parameter = "22cdbb33-b3bb-11ec-840c-94c691a734ac",
                    ResponseParseFunction = subFuncFleet_ReST_ParsingPositions,
                },
                new FleetMapRequest()
                {
                    Name = "GET_POSITIONS_ID",
                    Request = "api/v2.0.0/positions/{0}",
                    Parameter = "????????????", //pos_id 기입
                    ResponseParseFunction = subFuncFleet_ReST_ParsingPositionsID,
                },
                new FleetMapRequest()
                {
                    Name = "GET_ROBOTS",
                    Request = "api/v2.0.0/robots/",
                    Parameter = "",
                    ResponseParseFunction = subFuncFleet_ReST_ParsingRobotsID,
                },
                new FleetMapRequest()
                {
                    Name = "GET_ROBOT_ID",
                    Request = "api/v2.0.0/robots/{0}",
                    Parameter = "6",
                    ResponseParseFunction = subFuncFleet_ReST_ParsingRobotsID,
                },
            };

            foreach (var item in sampleMessages)
            {
                var msg = item;

                DoWork(ref msg);

                Console.WriteLine("message name = {0}", msg.Name);
                Console.WriteLine("message request = {0}", msg.RequestUriFull);
                Console.WriteLine("message response = \n{0}", msg.ResponseResult);
                Console.WriteLine();

                switch (msg.Name)
                {
                    case "GET_MAPS":
                        var maps = (List<FleetMap>)msg.ResponseResult;
                        break;

                    case "GET_MAPS_ID":
                        var map = (FleetMap)msg.ResponseResult;
                        break;

                    case "GET_MAPS_ID_POSITIONS":
                        var positions = (List<FleetPosition>)msg.ResponseResult;
                        break;

                    case "GET_POSITIONS_ID":
                        var position = (FleetPosition)msg.ResponseResult;
                        break;

                    case "GET_ROBOTS":
                        var robots = (List<FleetRobot>)msg.ResponseResult;
                        break;

                    case "GET_ROBOT_ID":
                        var robot = (FleetRobot)msg.ResponseResult;
                        break;

                    default:
                        break;
                }
            }
        }


        public void DoWork(ref FleetMapRequest rest1)
        {
            var client = new HttpClient();
            var header = new AuthenticationHeaderValue("Basic", "ZGlzdHJpYnV0b3I6NjJmMmYwZjFlZmYxMGQzMTUyYzk1ZjZmMDU5NjU3NmU0ODJiYjhlNDQ4MDY0MzNmNGNmOTI5NzkyODM0YjAxNA==");
            client.DefaultRequestHeaders.Authorization = header;
            client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromSeconds(int.Parse(sFleet_ResponseTime)); // 설정시간 이후에 타임아웃 에러
            client.BaseAddress = new Uri(uriString);

            string recvMessage = null;

            if (true) // test
            {
                // test =================================
                //switch (rest1.Name)
                //{
                //    case "GET_MAPS": recvMessage = File.ReadAllText("..\\..\\Monitor.Map\\json\\get_maps.json"); break;
                //    case "GET_MAPS_ID": recvMessage = File.ReadAllText("..\\..\\Monitor.Map\\json\\get_maps_id.json"); break;
                //    case "GET_MAPS_ID_POSITIONS": recvMessage = File.ReadAllText("..\\..\\Monitor.Map\\json\\get_maps_id_positions.json"); break;
                //    case "GET_POSITIONS_ID": recvMessage = File.ReadAllText("..\\..\\Monitor.Map\\json\\get_positions_id.json"); break;
                //    case "GET_ROBOTS": recvMessage = File.ReadAllText("..\\..\\Monitor.Map\\json\\get_robots.json"); break;
                //    case "GET_ROBOT_ID": recvMessage = File.ReadAllText("..\\..\\Monitor.Map\\json\\get_robots_id.json"); break;
                //    case "GET_ROBOTS_ID": recvMessage = File.ReadAllText("..\\..\\Monitor.Map\\json\\{0}.json".format(rest1.Name)); break;
                //}
                recvMessage = File.ReadAllText(string.Format("..\\..\\Monitor.Map\\json\\{0}.json", rest1.Name));
                rest1.ResponseResult = rest1.ResponseParseFunction(recvMessage);
                // test =================================
            }
            else
            {
                rest1.Response = client.GetAsync(rest1.RequestUriFull).Result;
                if (rest1.Response.IsSuccessStatusCode)
                {
                    recvMessage = rest1.Response.Content.ReadAsStringAsync().Result;
                    rest1.ResponseResult = rest1.ResponseParseFunction(recvMessage);
                }
                else
                {
                    logger.Info($"<Fleet>{rest1.Name} Error Log Code/Reason = " + (int)rest1.Response.StatusCode + "/" + rest1.Response.ReasonPhrase);
                }
            }
        }
    }
}
