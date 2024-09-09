using ACS.Monitor.Utilities;
using Monitor.Common;
using System;
using System.Collections.Generic;

namespace Monitor.Data
{
    public  class ConfigData
    {
        // UCMapView 모니터링에서 사용
        public static IList<Robot> Robots = null;
        public static IList<Job> Jobs = null;
        public static IList<FloorMapIdConfigModel> FloorMapIdConfigs = null;
        public static IList<CustomMapModel> CustomMaps = null;
        public static IList<FleetPositionModel> FleetPositions = null;

        public static Dictionary<string, string> DisplayRobotNames;
        public static Dictionary<string, string> DisplayMapNames;
        public static int MonitorPcList_MaxNum = 2;

        //MiR 개수 설정
        public const int MiR_MaxNum = 4;

        //Call Button 개수 설정
        public static int CallButton_MaxNum;

        public static int JobConfig_MaxNum;

        //User관련 설정
        public static string sAccessLevel = string.Empty;
        public static string sMaint_Password = string.Empty;
        public static string sEngineer_Password = string.Empty;
        public static string sOperator_TrafficSetting = string.Empty;
        public static string sOperator_MissionSetting = string.Empty;
        public static string sOperator_User = string.Empty;
        public static string sMaint_TrafficSetting = string.Empty;
        public static string sMaint_MissionSetting = string.Empty;
        public static string sMaint_User = string.Empty;

        //Fleet IP 설정 관련 변수 
        public static string sFleet_IP_Address_SV = string.Empty;                                    //Fleet IP 설정 설정 변수
        public static string sFleet_ResponseTime = string.Empty;                                     //Fleet 통신 응답시간 설정 변수   

        //Plc IP 설정 관련 변수
        public static string sPlc_IP_Address_SV = string.Empty;
        public static string sPlc_PortNumber = string.Empty;

        public static string sWise_IP_Address_SV = string.Empty;
        public static string sWise_ResponseTime = string.Empty;

        //MiR자동충전 관련 변수
        public static string sAutoChargeUse = string.Empty;                                          //자동충전 기능 사용 미사용 설정 변수
        public static string sAutoChargeStartPercent = string.Empty;                                 //자동충전 시작 배터리 퍼센트 설정 변수
        public static string sAutoChargeMissionEnablePercent = string.Empty;                         //자동충전중 미션가능 배터리 퍼센트 설정 변수
        public static string sAutoChargeDelayTime = string.Empty;                                    //자동충전 지연시간 설정 변수
        public static string sAutoChargeEndPercent = string.Empty;                                   //자동충전 완료 배터리 퍼센트 설정 변수
        public static string sAutoChargeMission = string.Empty;                                      //자동충전 미션 설정 변수

        public static int ChargingCount = 4;                                                              //자동충전 개수
        public static string[] ChargingPortMissions = new string[ChargingCount];                                //자동충전 미션 N 설정 변수
        public static bool[] ChargingPortEnable = new bool[ChargingCount];                                      //자동충전 스테이션 N 사용 여부 설정 변수

        public static string sAutoWaitingUse = string.Empty;                                         //자동대기 기능 사용 미사용 설정 변수
        public static string sAutoWaitingMission = string.Empty;                                     //자동대기 미션 설정 변수  
        public static string sReset_MiR_Mission = string.Empty;                                      //Reset MiR 미션 설정 변수  
        public static string sReset_MiR_No = string.Empty;                                           //Reset MiR 번호 설정 변수  
        public static string sReset_MiR_Name = string.Empty;                                         //Reset MiR 이름 설정 변수  

        //MiR Position Area 관련 변수
        public static string sAutoWaiting_Position_Use = string.Empty;                               //대기 충전 위치 설정변수 (사용/미사용)   
        public static double dAutoWaiting_Position_X1 = 0;                                           //대기 충전 위치 설정변수 (X1)   
        public static double dAutoWaiting_Position_X2 = 0;                                           //대기 충전 위치 설정변수 (X2)   
        public static double dAutoWaiting_Position_X3 = 0;                                           //대기 충전 위치 설정변수 (X3)   
        public static double dAutoWaiting_Position_X4 = 0;                                           //대기 충전 위치 설정변수 (X4)   
        public static double dAutoWaiting_Position_Y1 = 0;                                           //대기 충전 위치 설정변수 (Y1)   
        public static double dAutoWaiting_Position_Y2 = 0;                                           //대기 충전 위치 설정변수 (Y2)   
        public static double dAutoWaiting_Position_Y3 = 0;                                           //대기 충전 위치 설정변수 (Y3)   
        public static double dAutoWaiting_Position_Y4 = 0;                                           //대기 충전 위치 설정변수 (Y4)

        //Teaffic 위치 관련 변수
        public static int Traffic_Maxnum = 7 + 1;
        public static string[] sTraffic_Position_Area_Use = new string[Traffic_Maxnum];              //Teaffic 위치 설정변수 (사용/미사용)
        public static double[] dTraffic_Position_Area_X1 = new double[Traffic_Maxnum];               //Teaffic 위치 설정변수 (X1)
        public static double[] dTraffic_Position_Area_X2 = new double[Traffic_Maxnum];               //Teaffic 위치 설정변수 (X2)
        public static double[] dTraffic_Position_Area_X3 = new double[Traffic_Maxnum];               //Teaffic 위치 설정변수 (X3)
        public static double[] dTraffic_Position_Area_X4 = new double[Traffic_Maxnum];               //Teaffic 위치 설정변수 (X4)
        public static double[] dTraffic_Position_Area_Y1 = new double[Traffic_Maxnum];               //Teaffic 위치 설정변수 (Y1)
        public static double[] dTraffic_Position_Area_Y2 = new double[Traffic_Maxnum];               //Teaffic 위치 설정변수 (Y2)
        public static double[] dTraffic_Position_Area_Y3 = new double[Traffic_Maxnum];               //Teaffic 위치 설정변수 (Y3)
        public static double[] dTraffic_Position_Area_Y4 = new double[Traffic_Maxnum];               //Teaffic 위치 설정변수 (Y4)





        static ConfigData()
        {
            Load();
        }

        // 이 클래스가 생성되는 순간에 위의 필드 ConfigData.XXX 데이타를 한번에 모두 로드한다.
        private static void Load()
        {
            string tmp = AppConfiguration.GetAppConfig("RobotNames");
            ConfigData.DisplayRobotNames = Helpers.ConvertStringToDictionary(tmp) ?? new Dictionary<string, string>();


            ConfigData.CallButton_MaxNum = int.Parse(AppConfiguration.GetAppConfig("iCallButton_MaxNum"));

            //초기에 필요한 변수 설정값을 읽는 부분
            //AppConfiguration.ConfigDataSetting("sAccessLevel", "Operator"); // 프로그램 구동시 "Operator" 로 설정한다
            ConfigData.sAccessLevel = AppConfiguration.GetAppConfig("sAccessLevel");

            ConfigData.sMaint_Password = AppConfiguration.GetAppConfig("sMaint_Password");
            ConfigData.sEngineer_Password = AppConfiguration.GetAppConfig("sEngineer_Password");
            ConfigData.sOperator_MissionSetting = AppConfiguration.GetAppConfig("sOperator_MissionSetting");
            ConfigData.sOperator_TrafficSetting = AppConfiguration.GetAppConfig("sOperator_TrafficSetting");
            ConfigData.sOperator_User = AppConfiguration.GetAppConfig("sOperator_User");
            ConfigData.sMaint_MissionSetting = AppConfiguration.GetAppConfig("sMaint_MissionSetting");
            ConfigData.sMaint_TrafficSetting = AppConfiguration.GetAppConfig("sMaint_TrafficSetting");
            ConfigData.sMaint_User = AppConfiguration.GetAppConfig("sMaint_User");

            //Fleet 기본 Setting 값 불러오는 부분
            ConfigData.sFleet_ResponseTime = AppConfiguration.GetAppConfig("sFleet_ResponseTime");
            ConfigData.sFleet_IP_Address_SV = AppConfiguration.GetAppConfig("sFleet_IP_Address_SV");
            ConfigData.sPlc_IP_Address_SV = AppConfiguration.GetAppConfig("sPlc_IP_Address_SV");
            ConfigData.sPlc_PortNumber = AppConfiguration.GetAppConfig("sPlc_PortNumber");
            ConfigData.sWise_IP_Address_SV = AppConfiguration.GetAppConfig("sWise_IP_Address_SV");
            ConfigData.sWise_ResponseTime = AppConfiguration.GetAppConfig("sWise_ResponseTime");

            ConfigData.sAutoChargeUse = AppConfiguration.GetAppConfig("sAutoChargeUse");
            ConfigData.sAutoWaitingUse = AppConfiguration.GetAppConfig("sAutoWaitingUse");
            ConfigData.sAutoChargeStartPercent = AppConfiguration.GetAppConfig("sAutoChargeStartPercent");
            ConfigData.sAutoChargeEndPercent = AppConfiguration.GetAppConfig("sAutoChargeEndPercent");
            ConfigData.sAutoChargeDelayTime = AppConfiguration.GetAppConfig("sAutoChargeDelayTime");
            ConfigData.sAutoChargeMissionEnablePercent = AppConfiguration.GetAppConfig("sAutoChargeMissionEnablePercent");

            ConfigData.dAutoWaiting_Position_X1 = double.Parse(AppConfiguration.GetAppConfig("dAutoWaiting_Position_X1"));
            ConfigData.dAutoWaiting_Position_X2 = double.Parse(AppConfiguration.GetAppConfig("dAutoWaiting_Position_X2"));
            ConfigData.dAutoWaiting_Position_X3 = double.Parse(AppConfiguration.GetAppConfig("dAutoWaiting_Position_X3"));
            ConfigData.dAutoWaiting_Position_X4 = double.Parse(AppConfiguration.GetAppConfig("dAutoWaiting_Position_X4"));
            ConfigData.dAutoWaiting_Position_Y1 = double.Parse(AppConfiguration.GetAppConfig("dAutoWaiting_Position_Y1"));
            ConfigData.dAutoWaiting_Position_Y2 = double.Parse(AppConfiguration.GetAppConfig("dAutoWaiting_Position_Y2"));
            ConfigData.dAutoWaiting_Position_Y3 = double.Parse(AppConfiguration.GetAppConfig("dAutoWaiting_Position_Y3"));
            ConfigData.dAutoWaiting_Position_Y4 = double.Parse(AppConfiguration.GetAppConfig("dAutoWaiting_Position_Y4"));

            ConfigData.sAutoChargeMission = AppConfiguration.GetAppConfig("sAutoChargeMission");
            ConfigData.sAutoWaitingMission = AppConfiguration.GetAppConfig("sAutoWaitingMission");
            ConfigData.sReset_MiR_Mission = AppConfiguration.GetAppConfig("sReset_MiR_Mission");

            for (int i = 0; i < ConfigData.ChargingCount; i++)
            {
                ConfigData.ChargingPortEnable[i] = AppConfiguration.GetAppConfig($"ChargingPortEnable{i + 1}").ToUpper() == "USE" ? true : false;
                ConfigData.ChargingPortMissions[i] = AppConfiguration.GetAppConfig($"ChargingPortMissions{i + 1}");
            }


            for (int i = 1; i < ConfigData.Traffic_Maxnum; i++)
            {
                ConfigData.sTraffic_Position_Area_Use[i] = AppConfiguration.GetAppConfig("sTraffic_Position_Area_Use" + "Index = " + i);
                ConfigData.dTraffic_Position_Area_X1[i] = double.Parse(AppConfiguration.GetAppConfig("dTraffic_Position_Area_X1" + "Index = " + i));
                ConfigData.dTraffic_Position_Area_X2[i] = double.Parse(AppConfiguration.GetAppConfig("dTraffic_Position_Area_X2" + "Index = " + i));
                ConfigData.dTraffic_Position_Area_X3[i] = double.Parse(AppConfiguration.GetAppConfig("dTraffic_Position_Area_X3" + "Index = " + i));
                ConfigData.dTraffic_Position_Area_X4[i] = double.Parse(AppConfiguration.GetAppConfig("dTraffic_Position_Area_X4" + "Index = " + i));
                ConfigData.dTraffic_Position_Area_Y1[i] = double.Parse(AppConfiguration.GetAppConfig("dTraffic_Position_Area_Y1" + "Index = " + i));
                ConfigData.dTraffic_Position_Area_Y2[i] = double.Parse(AppConfiguration.GetAppConfig("dTraffic_Position_Area_Y2" + "Index = " + i));
                ConfigData.dTraffic_Position_Area_Y3[i] = double.Parse(AppConfiguration.GetAppConfig("dTraffic_Position_Area_Y3" + "Index = " + i));
                ConfigData.dTraffic_Position_Area_Y4[i] = double.Parse(AppConfiguration.GetAppConfig("dTraffic_Position_Area_Y4" + "Index = " + i));
            }
        }

        // 데이터 저장부분은 SystenScreen 에서 개별적으로 처리된다...
        private static void Save()
        {
            throw new NotImplementedException();
            //..... string newValue = "...";
            // .... AppConfiguration.ConfigDataSetting("XXX", newValue);
        }
    }
}
