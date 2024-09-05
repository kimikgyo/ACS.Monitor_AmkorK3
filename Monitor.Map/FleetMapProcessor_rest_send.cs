using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Monitor.Map
{
    public partial class FleetMapProcessor
    {
        #region Fleet REST send
        private object _Thread_Fleet_ReST_Send(string sRequestType, string sValue1, string sValue2, string sPostMissionIndex)
        {
            object result = null;
            HttpClient client = null;
            HttpResponseMessage response = null;
            try
            {
                client = new HttpClient();
                var header = new AuthenticationHeaderValue("Basic", "ZGlzdHJpYnV0b3I6NjJmMmYwZjFlZmYxMGQzMTUyYzk1ZjZmMDU5NjU3NmU0ODJiYjhlNDQ4MDY0MzNmNGNmOTI5NzkyODM0YjAxNA==");
                client.DefaultRequestHeaders.Authorization = header;
                client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en_US"));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.Timeout = TimeSpan.FromSeconds(int.Parse(sFleet_ResponseTime)); // 설정시간 이후에 타임아웃 에러
                client.BaseAddress = new Uri(uriString);

                string requestUri = string.Empty;
                string recvMessage = string.Empty;

                switch (sRequestType)
                {
                    case "GET_MAPS":
                        requestUri = $"api/v2.0.0/maps";
                        response = client.GetAsync(requestUri).Result;
                        response.EnsureSuccessStatusCode();
                        recvMessage = response.Content.ReadAsStringAsync().Result;
                        result = subFuncFleet_ReST_ParsingMaps(recvMessage);
                        break;

                    case "GET_MAPS_ID":
                        requestUri = $"api/v2.0.0/maps/{sValue1}";
                        response = client.GetAsync(requestUri).Result;
                        response.EnsureSuccessStatusCode();
                        recvMessage = response.Content.ReadAsStringAsync().Result;
                        result = subFuncFleet_ReST_ParsingMapsID(recvMessage);
                        break;

                    case "GET_MAPS_ID_POSITIONS":
                        requestUri = $"api/v2.0.0/maps/{sValue1}/positions";
                        response = client.GetAsync(requestUri).Result;
                        response.EnsureSuccessStatusCode();
                        recvMessage = response.Content.ReadAsStringAsync().Result;
                        result = subFuncFleet_ReST_ParsingPositions(recvMessage);
                        break;

                    case "GET_POSITIONS_ID":
                        requestUri = $"api/v2.0.0/positions/{sValue1}";
                        response = client.GetAsync(requestUri).Result;
                        response.EnsureSuccessStatusCode();
                        recvMessage = response.Content.ReadAsStringAsync().Result;
                        result = subFuncFleet_ReST_ParsingPositionsID(recvMessage);
                        break;

                    case "GET_ROBOTS":
                        requestUri = $"api/v2.0.0/robots";
                        response = client.GetAsync(requestUri).Result;
                        response.EnsureSuccessStatusCode();
                        recvMessage = response.Content.ReadAsStringAsync().Result;
                        result = subFuncFleet_ReST_ParsingRobots(recvMessage);
                        break;

                    case "GET_ROBOT_ID":
                        requestUri = $"api/v2.0.0/robots/" + sValue1;
                        response = client.GetAsync(requestUri).Result;
                        response.EnsureSuccessStatusCode();
                        recvMessage = response.Content.ReadAsStringAsync().Result;
                        result = subFuncFleet_ReST_ParsingRobotsID(recvMessage);
                        break;
                }

                //// 통신 데이터 로깅
                //if (requestUri != null)
                //{
                //    Directory.CreateDirectory(@"c:/temp");
                //    //string fileName = @"c:/temp/map_data.txt";
                //    string fileName = $@"c:/temp/map_data_{this.MapName}.txt";
                //    string TimeText = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //    File.AppendAllText(fileName, $"{TimeText},{requestUri},{recvMessage}");
                //}

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                if (response != null)
                    logger?.Info($"<Fleet> {sRequestType} Error Log Code/Reason = " + (int)response.StatusCode + "/" + response.ReasonPhrase);
                else if (response == null)
                    logger?.Info($"<Fleet> {sRequestType} Error");
                return null;
            }
            finally
            {
                client.Dispose();
            }

            return result;
        }
        #endregion
    }
}
