using Monitor.Common;
using System.Collections.Generic;

namespace ACS.Monitor
{
    public partial class MainForm
    {
        //Log Viewer 관련
        public string[] ACS_Array_Log_Text = new string[1000];
        public bool bACSLogRefresh = false;

        //Fleet 연결 확인 설정 변수
        public bool bFleetConnected = false;

        //==========================================================


        //Fleet에서 읽은 자동충전 미션시작 배터리 양
        public string sFleetGetMissionStaet_battery = string.Empty;


        //읽어온 MissionGroup 정보
        public IList<MissionJson> GetMissionGroups = new List<MissionJson>(); // =========> 사용x


        //읽어온 Mission 정보
        public IList<MissionJson> GetMissions = new List<MissionJson>();


        //읽어온 Mission Schedule 정보 (mir에 의해 실행되는 Mission을 관리하는 객체)
        public IList<MissionSchedulerJson> MissionSchedulers = new List<MissionSchedulerJson>(); // the list of missions queued in the mission scheduler
        public MissionSchedulerJson MissionScheduler; // Retrieve the details about the mission scheduler with the specified id

        // job test flag
        public bool jobStepFlag = false;

        //========================================================== Alarm 팝업 관련
        public AlarmMessageQueue<string> AlarmMessageQueue = new AlarmMessageQueue<string>();

        ////==========================================================


        ////Call Button관련 변수
        //public int TCP_Connect_Client { get; set; } = 0;                    // =========> 사용x


        ////========================================================== POPCALL 팝업 관련
        //public ConcurrentQueue<string> PopCallErrorMessageQueue = new ConcurrentQueue<string>();


        ////========================================================== WiseModule 출고랙 관련
        //public int[] Wise_OutputSignal_Write_Flag = new int[4];
        //public string[] sWise_OutputSignal_Write_Ch = new string[4];
        //public string[] sWise_OutputSignal_Write_Value = new string[4];

        //public bool Test_WiseEvent_EmptyConveyor_InputReady = false;


    }
}
