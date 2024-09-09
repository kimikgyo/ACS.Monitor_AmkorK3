using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monitor.Common
{
    public enum FleetState
    {
        None = 0,
        unavailable = 1,
        version_mismatch = 2,
        sw_guid_mismatch = 3,
        emergency_stop = 4,
        error = 5,
        starting = 6,
        manual_control = 8,
        pause = 9,
        low_battery = 10,
        ready = 11,
        charging = 12,
        staging = 13,
        independent_mission = 14,
        executing_fleet_order = 15,
        unknown = 16,
        crashed = 17,
        synchronizing = 18,
        initial_synchronization = 19,
        evacuating = 20,
        evacuated = 21,
        thread_died = 22,
        Factory_reset_failed = 23,
        Factory_Resetting = 24,
        deactivated = 25,
    }

    public enum RobotState
    {
        None = 0,
        Starting = 1,
        ShuttingDown = 2,
        Ready = 3,
        Pause = 4,
        Executing = 5,
        Aborted = 6,
        Completed = 7,
        Docked = 8,
        Docking = 9,
        EmergencyStop = 10,
        ManualControl = 11,
        Error = 12,
    }

    public enum RobotType
    {
        LIFT = 0,
        HOOK = 1,
    }


    //MiR Status 저장관련 변수
    public class Robot
    {
        public bool DataChanged = false;




        private int id;
        private int jobId;
        private string aCSRobotGroup;           //Robot ACSMissionGroup 를 나누어 미션을 전송하기 위한 변수 
        private bool aCSRobotActive;            // robot 사용 여부
        private int robotID;                    // robot id (fleet only)
        private string robotIp;                 // robot ip
        private string robotName;               // robot name
        private string robotAlias;              // robot alias
        private RobotState stateID;             // MiR의 상태(Text)
        private string stateText;               // MiR의 상태(ID)
        private string missionText;             // MiR의 Mission Text
        private int missionQueueID;             // MiR의 현재 Mission Queue ID
        private double batteryPercent;          // MiR의 Battery Percent(Text)
        private string mapID;                   // map id
        private double distanceToNextTarget;    // MiR의 다음 타겟까지의 거리
        private double position_Orientation;    // MiR의 Position R Value
        private double position_X;              // MiR의 Position X Value
        private double position_Y;              // MiR의 Position Y Value
        private string robotModel;              // MiR의 Model Name ex. "MiR100"
        private string errors;                  // MiR의 Error List
        private string product;                 // AmkorK5 사용 Line 자재 확인 
        private string door;                    // AmkorK5 사용 Line TopModule Door 상태
        private string positionZoneName;
        private int progressBar;

        public RobotType RobotType = RobotType.LIFT;     // 0=LIFT타입, 1=HOOK타입
        private FleetState fleet_State;              // fleet 상태
        private string fleet_State_Text;             // fleet 상태
        private bool connectState;                 // RobotConnect 상태
        public int Id
        {
            get => id;
            set
            {
                if (id != value) DataChanged = true;
                id = value;
            }
        }
        public int JobId
        {
            get => jobId;
            set
            {
                if (jobId != value) DataChanged = true;
                jobId = value;
            }
        }
        public string ACSRobotGroup
        {
            get => aCSRobotGroup;
            set
            {
                if (aCSRobotGroup != value) DataChanged = true;
                aCSRobotGroup = value;
            }
        }
        public bool ACSRobotActive
        {
            get => aCSRobotActive;
            set
            {
                if (aCSRobotActive != value) DataChanged = true;
                aCSRobotActive = value;
            }
        }


        // robot info
        public int RobotID
        {
            get => robotID;
            set
            {
                if (robotID != value) DataChanged = true;
                robotID = value;
            }
        }
        public string RobotIp
        {
            get => robotIp;
            set
            {
                if (robotIp != value) DataChanged = true;
                robotIp = value;
            }
        }


        // robot status
        public string RobotName
        {
            get => robotName;
            set
            {
                if (robotName != value) DataChanged = true;
                robotName = value;
            }
        }
        public string RobotAlias
        {
            get => robotAlias;
            set
            {
                if (robotAlias != value) DataChanged = true;
                robotAlias = value;
            }
        }
        public RobotState StateID
        {
            get => stateID;
            set
            {
                if (stateID != value) DataChanged = true;
                stateID = value;
            }
        }
        public string StateText
        {
            get => stateText;
            set
            {
                if (stateText != value) DataChanged = true;
                stateText = value;
            }
        }
        public FleetState Fleet_State
        {
            get => fleet_State;
            set
            {
                if (fleet_State != value) DataChanged = true;
                fleet_State = value;
            }
        }
        public string Fleet_State_Text
        {
            get => fleet_State_Text;
            set
            {
                if (fleet_State_Text != value) DataChanged = true;
                fleet_State_Text = value;
            }
        }

        public string MissionText
        {
            get => missionText;
            set
            {
                if (missionText != value) DataChanged = true;
                missionText = value;
            }
        }
        public int MissionQueueID
        {
            get => missionQueueID;
            set
            {
                if (missionQueueID != value) DataChanged = true;
                missionQueueID = value;
            }
        }
        public double BatteryPercent
        {
            get => batteryPercent;
            set
            {
                double roundedValue = Math.Round(value, 2);
                if (batteryPercent != roundedValue) DataChanged = true;
                batteryPercent = roundedValue;
            }
        }
        public string MapID
        {
            get => mapID;
            set
            {
                if (mapID != value) DataChanged = true;
                mapID = value;
            }
        }
        public double DistanceToNextTarget
        {
            get => distanceToNextTarget;
            set
            {
                double roundedValue = Math.Round(value, 2);
                if (distanceToNextTarget != roundedValue) DataChanged = true;
                distanceToNextTarget = roundedValue;
            }
        }
        public double Position_Orientation
        {
            get => position_Orientation;
            set
            {
                double roundedValue = Math.Round(value, 2);
                if (position_Orientation != roundedValue) DataChanged = true;
                position_Orientation = roundedValue;
            }
        }
        public double Position_X
        {
            get => position_X;
            set
            {
                double roundedValue = Math.Round(value, 2);
                if (position_X != roundedValue) DataChanged = true;
                position_X = roundedValue;
            }
        }
        public double Position_Y
        {
            get => position_Y;
            set
            {
                double roundedValue = Math.Round(value, 2);
                if (position_Y != roundedValue) DataChanged = true;
                position_Y = roundedValue;
            }
        }

        // 로봇 에러 정보 ==========BEGIN
        // - Errors
        // - ErrorsJson : robots 테이블 컬럼 추가 : alter table robots add ErrorsJson varchar(5000) null

        public RobotErrorJson[] Errors; // 로봇 에러 리스트. 이 필드는 DB에 저장하지 않는다!

        public string ErrorsJson        // 로봇 에러 리스트 json.
        {
            get => errors;
            set
            {
                if (errors != value) DataChanged = true;
                errors = value;
            }
        }

        public string ErrorMailState = ""; // 로봇에러발생시 이메일전송진행상태 플래그 (mail-sent=전송했음, no-error=에러없음)

        public bool POSTimeOutFlag = false;
        public double POSTimeOutError_X = 0;
        public double POSTimeOutError_Y = 0;
        public DateTime POSTimeOutError_Time;

        // 로봇 에러 정보 ==========END

        public string RobotModel
        {
            get => robotModel;
            set
            {
                if (robotModel != value) DataChanged = true;
                robotModel = value;
            }
        }

        public string Product
        {
            get => product;
            set
            {
                if (product != value) DataChanged = true;
                product = value;
            }
        }
        public string Door
        {
            get => door;
            set
            {
                if (door != value) DataChanged = true;
                door = value;
            }
        }
        public string PositionZoneName
        {
            get => positionZoneName;
            set
            {
                if (positionZoneName != value) DataChanged = true;
                positionZoneName = value;
            }
        }
        public int ProgressBar
        {
            get => progressBar;
            set
            {
                if (progressBar != value) DataChanged = true;
                progressBar = value;
            }
        }

        public string LastCallName { get; set; }    // 로봇이 수행한 마지막 콜을 기억해둔다

        // fleet status
        //public FleetState Fleet_State;              // fleet 상태
        //public string Fleet_State_Text;             // fleet 상태


        // robot registers
        public RobotRegisters Registers = new RobotRegisters(); // 레지스터.  데이터 갱신은 MiR_Get_Register()함수 이용한다.
        public RoBotIoModules internalIoModule = new RoBotIoModules(); //Robot 내부 IoModule




        public override string ToString()
        {
            //return JsonConvert.SerializeObject(this);
            return $"id={Id}, RobotID={RobotID}, Name={RobotName}, JobID={JobId}" +
                $", stateID={StateID}" +
                //$", stateText={StateText}" +
                $", Group={ACSRobotGroup}" +
                $", MissionQueueID={missionQueueID,4}" +
                $", Battery={BatteryPercent}" +
                //$", DistanceToNextTarget={DistanceToNextTarget}" +
                $", Theta={Position_Orientation}, X={position_X}, Y={position_Y}" +
                $", MissionText={MissionText}" +
                "";
        }
    }

    public class RoBotIoModules
    {
        public string type = "internal";
        public string guid;
        public bool Get_InputValue1;    //자채 IO Module Get_InputValue1 = 자재 무(false) 유(true)
        public bool Get_InputValue2;    //자채 IO Module Get_InputValue2 = door 열림 (false) 닫힘 (true)
        public bool Get_InputValue3;
        public bool Get_InputValue4;
        public bool Get_OutputValue1;
        public bool Get_OutputValue2;
        public bool Get_OutputValue3;
        public bool Get_OutputValue4;
    }





    public class RobotRegisters
    {

        public double[] dMiR_Register_Value = new double[100];



        //MiR_Register 1~19까지 PLC에서 사용
        //MiR_Register 20~39 까지 ACS에서 사용
        //MiR_Register 전체 MiR의 지정된 Register 번호
        //public double dMiR_Register_Value1;                        //(PLC)MiR 레지스터_1 불러오는 변수 
        //public double dMiR_Register_Value2;                        //(PLC)MiR 레지스터_2 불러오는 변수
        //public double dMiR_Register_Value3;                        //(PLC)MiR 레지스터_3 불러오는 변수
        //public double dMiR_Register_Value4;                        //(PLC)MiR 레지스터_4 불러오는 변수
        //public double dMiR_Register_Value5;                        //(PLC)MiR 레지스터_5 불러오는 변수
        //public double dMiR_Register_Value6;                        //(PLC)MiR 레지스터_6 불러오는 변수
        //public double dMiR_Register_Value7;                        //(PLC)MiR 레지스터_7 불러오는 변수
        //public double dMiR_Register_Value8;                        //(PLC)MiR 레지스터_8 불러오는 변수
        //public double dMiR_Register_Value9;                        //(PLC)MiR 레지스터_9 불러오는 변수
        //public double dMiR_Register_Value10;                       //(PLC)MiR 레지스터_10 불러오는 변수
        //public double dMiR_Register_Value11;                       //(PLC)MiR 레지스터_11 불러오는 변수
        //public double dMiR_Register_Value12;                       //(PLC)MiR 레지스터_12 불러오는 변수
        //public double dMiR_Register_Value13;                       //(PLC)MiR 레지스터_13 불러오는 변수
        //public double dMiR_Register_Value14;                       //(PLC)MiR 레지스터_14 불러오는 변수
        //public double dMiR_Register_Value15;                       //(PLC)MiR 레지스터_15 불러오는 변수
        //public double dMiR_Register_Value16;                       //(PLC)MiR 레지스터_16 불러오는 변수
        //public double dMiR_Register_Value17;                       //(PLC)MiR 레지스터_17 불러오는 변수
        //public double dMiR_Register_Value18;                       //(PLC)MiR 레지스터_18 불러오는 변수
        //public double dMiR_Register_Value19;                       //(PLC)MiR 레지스터_19 불러오는 변수



        //ACS 사용 영역
        //public double dMiR_Register_Value20;                        //(ACS)MiR 레지스터_20 불러오는 변수
        //public double dMiR_Register_Value21;                        //(ACS)MiR 레지스터_21 불러오는 변수 
        //public double dMiR_Register_Value22;                        //(ACS)MiR 레지스터_22 불러오는 변수
        //public double dMiR_Register_Value23;                        //(ACS)MiR 레지스터_23 불러오는 변수
        //public double dMiR_Register_Value24;                        //(ACS)MiR 레지스터_24 불러오는 변수
        //public double dMiR_Register_Value25;                        //(ACS)MiR 레지스터_25 불러오는 변수
        //public double dMiR_Register_Value26;                        //(ACS)MiR 레지스터_26 불러오는 변수
        //public double dMiR_Register_Value27;                        //(ACS)MiR 레지스터_27 불러오는 변수
        //public double dMiR_Register_Value28;                        //(ACS)MiR 레지스터_28 불러오는 변수
        //public double dMiR_Register_Value29;                        //(ACS)MiR 레지스터_29 불러오는 변수
        //public double dMiR_Register_Value30;                        //(ACS)MiR 레지스터_30 불러오는 변수
        //public double dMiR_Register_Value31;                        //(ACS)MiR 레지스터_31 불러오는 변수
        //public double dMiR_Register_Value32;                        //(ACS)MiR 레지스터_32 불러오는 변수
        //public double dMiR_Register_Value33;                        //(ACS)MiR 레지스터_33 불러오는 변수
        //public double dMiR_Register_Value34;                        //(ACS)MiR 레지스터_34 불러오는 변수
        //public double dMiR_Register_Value35;                        //(ACS)MiR 레지스터_35 불러오는 변수
        //public double dMiR_Register_Value36;                        //(ACS)MiR 레지스터_36 불러오는 변수
        //public double dMiR_Register_Value37;                        //(ACS)MiR 레지스터_37 불러오는 변수
        //public double dMiR_Register_Value38;                        //(ACS)MiR 레지스터_38 불러오는 변수
        //public double dMiR_Register_Value39;                        //(ACS)MiR 레지스터_39 불러오는 변수
        //public double dMiR_Register_Value40;                        //(ACS)MiR 레지스터_40 불러오는 변수
        //public double dMiR_Register_Value70;                        //(ACS)MiR 레지스터_70 불러오는 변수
        //public double dMiR_Register_Value71;                        //(ACS)MiR 레지스터_71 불러오는 변수
        //public double dMiR_Register_Value72;                        //(ACS)MiR 레지스터_72 불러오는 변수
        //public double dMiR_Register_Value73;                        //(ACS)MiR 레지스터_73 불러오는 변수
        //public double dMiR_Register_Value74;                        //(ACS)MiR 레지스터_74 불러오는 변수
        //public double dMiR_Register_Value75;                        //(ACS)MiR 레지스터_75 불러오는 변수
        //public double dMiR_Register_Value76;                        //(ACS)MiR 레지스터_76 불러오는 변수
        //public double dMiR_Register_Value77;                        //(ACS)MiR 레지스터_77 불러오는 변수
        //public double dMiR_Register_Value78;                        //(ACS)MiR 레지스터_78 불러오는 변수
        //public double dMiR_Register_Value79;                        //(ACS)MiR 레지스터_79 불러오는 변수
        //public double dMiR_Register_Value80;                        //(ACS)MiR 레지스터_80 불러오는 변수
        //public double dMiR_Register_Value81;                        //(ACS)MiR 레지스터_71 불러오는 변수
        //public double dMiR_Register_Value82;                        //(ACS)MiR 레지스터_72 불러오는 변수
        //public double dMiR_Register_Value83;                        //(ACS)MiR 레지스터_73 불러오는 변수
        //public double dMiR_Register_Value84;                        //(ACS)MiR 레지스터_74 불러오는 변수
        //public double dMiR_Register_Value85;                        //(ACS)MiR 레지스터_75 불러오는 변수
        //public double dMiR_Register_Value86;                        //(ACS)MiR 레지스터_76 불러오는 변수
        //public double dMiR_Register_Value87;                        //(ACS)MiR 레지스터_77 불러오는 변수
        //public double dMiR_Register_Value88;                        //(ACS)MiR 레지스터_78 불러오는 변수
        //public double dMiR_Register_Value89;                        //(ACS)MiR 레지스터_79 불러오는 변수

        //public double dMiR_Register_Value97;                        //(ACS)MiR 레지스터_40 불러오는 변수
        //public double dMiR_Register_Value98;                        //(ACS)MiR 레지스터_40 불러오는 변수

        //Register Sync(전체 MiR의 지정된 Register 번호를 동기화)
        //public double dMiR_Register_Sync41;                        //MiR 레지스터_41 동기화 설정 변수
        //public double dMiR_Register_Sync42;                        //MiR 레지스터_42 동기화 설정 변수
        //public double dMiR_Register_Sync43;                        //MiR 레지스터_43 동기화 설정 변수
        //public double dMiR_Register_Sync44;                        //MiR 레지스터_44 동기화 설정 변수
        //public double dMiR_Register_Sync45;                        //MiR 레지스터_45 동기화 설정 변수
        //public double dMiR_Register_Sync46;                        //MiR 레지스터_46 동기화 설정 변수
        //public double dMiR_Register_Sync47;                        //MiR 레지스터_47 동기화 설정 변수
        //public double dMiR_Register_Sync48;                        //MiR 레지스터_48 동기화 설정 변수
        //public double dMiR_Register_Sync49;                        //MiR 레지스터_49 동기화 설정 변수
        //public double dMiR_Register_Sync50;                        //MiR 레지스터_50 동기화 설정 변수
    }
}
