using Newtonsoft.Json;
using System;

namespace Monitor.Common
{
    public class MissionJson
    {
        /*
            "guid": "47ffbd03-cabb-11ec-8a38-94c691a7389a",
            "name": "MyMission",
        */
        public string guid;
        public string name;
        public override string ToString() => $"{guid} : {name}";
    }



    public class FleetRobotInfoJson
    {
        public int id { get; set; }
        public string ip { get; set; }
        public FleetState fleet_state { get; set; }
        public string fleet_state_text { get; set; }
        public string robot_model { get; set; }
        public RobotStatusJson status { get; set; }
    }
    public class RobotStatusJson
    {
        public string robot_name { get; set; }
        public RobotState state_id { get; set; }
        public string state_text { get; set; }
        public string mission_text { get; set; }
        public int mission_queue_id { get; set; }
        public double battery_percentage { get; set; }
        public double distance_to_next_target { get; set; }
        public string map_id { get; set; }
        public RobotErrorJson[] errors { get; set; }
        public RobotPositionJson position { get; set; }
    }
    public class RobotErrorJson
    {
        public int Code { get; set; }
        public string Description { get; set; }
        public string Module { get; set; }
    }
    public class RobotPositionJson
    {
        public double x { get; set; }
        public double y { get; set; }
        public double orientation { get; set; }
    }



    public class SettingJson
    {
        /*
            "name": "Minimum battery percentage for release",
            "default": "60",
            "value": "60",
            "id": 2085
        */
        public string name;
        public string @default;
        public string value;
        public int id;
    }



    public class MissionSchedulerJson
    {
        /*
        Fleet
            "id": 542
            "state": "Pending",
            "order_time": "2022-05-31T17:44:24",
            "finish_time": "1970-01-01T00:00:00",
            "earliest_start_time": "2022-05-31T17:44:24",
            "start_time": "1970-01-01T00:00:00",
            "mission_id": "47ffbd03-cabb-11ec-8a38-94c691a7389a",
            "mission_name": "YKK_2",
            "priority": 0,
            "high_priority": true,
            "robot_id": 14,
        MiR
            {"priority": 0, "ordered": "2022-08-03T09:25:01", "description": "", "parameters": [], "state": "Executing", "started": "2022-08-03T09:25:01", "created_by_name": "Administrator", "mission": "/v2.0.0/missions/900a76ed-d0fe-11ec-b651-94c691a7389a", "actions": "/v2.0.0/mission_queue/723/actions", "fleet_schedule_guid": "", "mission_id": "900a76ed-d0fe-11ec-b651-94c691a7389a", "finished": null, "created_by": "/v2.0.0/users/mirconst-guid-0000-0005-users0000000", "created_by_id": "mirconst-guid-0000-0005-users0000000", "allowed_methods": ["PUT", "GET", "DELETE"], "message": "", "control_state": 0, "id": 723, "control_posid": "0"}
            ??
        */
        public int id;                  //Post Mission 시 MiR에서 반환된 ID 설정 변수
        public string state;
        public int robot_id;            //Post Mission 시 사용된 Robot ID 설정 변수
        //
        public string mission_id;
        public string mission_name;             // fleet only
        public bool high_priority;
        public DateTime? order_time;            // fleet only
        public DateTime? finish_time;           // fleet only
        public DateTime? earliest_start_time;   // fleet only
        public DateTime? start_time;            // fleet only
        //
        public DateTime? ordered;               // mir only
        public DateTime? finished;              // mir only
        public DateTime? started;               // mir only

        public override string ToString() => JsonConvert.SerializeObject(this);
    }



    /*
     * position type_id
     * 0: normal pos 일반포지션
     * 7: charging pos 충전포지션
     * 8: charging entry pos 충전진입포지션 (parent_id가 충전포지션id를 가리킨다)
     * 
     * 
     */

}








/*
 * GetMission_scheduler{
description	string
Inherited from mission description, when item was queued

order_time	string($date-time)
The date and time when the mission was scheduled

earliest_start_time	string($date-time)
The date and time at which the mission should start at the earliest

start_time	string($date-time)
The date and time when the mission was started

mission	string
The url to the mission that was scheduled

mission_id	string
The id identifying the mission scheduled by the scheduler

high_priority	boolean
The urgency of the mission scheduled

id	integer
The id of the mission schedule entry

parameters	string
finish_time	string($date-time)
The date and time when the mission was finished

created_by	string
The url to the description of the type of this element

fleet_schedule_guid	string
Unique identifier for the mission schedule element

priority	integer
The priority of the mission scheduled

state	string
The state of the

mission_name	string
The name of the mission scheduled by the scheduler

robot_id	integer
The id of the robot to which the mission was assigned

created_by_id	string
The global id of the user who created this entry

}
*/