﻿using ACS.Monitor.Utilities;
using Monitor.Common;
using System;
using System.Collections.Generic;

namespace Monitor.Data
{
    public  class ConfigData
    {
        public static IList<Robot> Robots = null;
        public static IList<Job> Jobs = null;
        public static IList<Mission> Missions = null;
        public static IList<PositionWaitTimeModel> PositionWaitTimes = null;
        public static IList<FloorMapIdConfigModel> FloorMapIdConfigs = null;
        public static IList<PositionAreaConfig> PositionAreaConfigs = null;
        public static IList<CustomMapModel> CustomMaps = null;
        public static IList<FleetPositionModel> FleetPositions = null;
        public static IList<ElevatorStateModel> ElevatorStates = null;
        public static IList<ElevatorInfoModel> ElevatorInfos = null;
        public static IList<MissionsSpecific> MissionsSpecifics = null;
        public static IList<JobConfigModel> JobConfigs = null;
        public static Dictionary<string, string> DisplayRobotNames;
        public static Dictionary<string, string> DisplayMapNames;


        public static bool MapViewScreenActive = false;
        public static bool SettingScreenActive = false;
        public static bool CallScreenActive = false;
        public static bool RobotScreenActive = false;
        public static bool ElevatorScreenActive = false;

        public static string UserNumber = null;   //LogIn 정보 {사번}
        public static string UserName = null;     //LogIn 정보 {사용자이름}
        public static string StartZone = null;    //User가 선택한 StartZone              
        public static int UserElevatorAuthority;  //Elevator {사용권한}
        public static int UserCallAuthority;      //CallSystem {사용권한}




        static ConfigData()
        {
            Load();
        }

        // 이 클래스가 생성되는 순간에 위의 필드 ConfigData.XXX 데이타를 한번에 모두 로드한다.
        private static void Load()
        {
            string tmp = AppConfiguration.GetAppConfig("RobotNames");
            ConfigData.DisplayRobotNames = Helpers.ConvertStringToDictionary(tmp) ?? new Dictionary<string, string>();
            
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
