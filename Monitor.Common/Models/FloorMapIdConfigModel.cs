using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Common
{
    public class FloorMapIdConfigModel
    {
        //============================================Robot Line 층 관련 정보(설정)
        public int Id { get; set; }
        public string FloorIndex { get; set; }         //층설정 (2024.05.14 수정)
        public string FloorName { get; set; }       //층이름설정
        public string MapID { get; set; }           //MapId 설정(RobotMapId를 확인후 설정한다)
        public string MapImageData { get; set; }    //Map Image Data
        public int DisplayFlag { get; set; }        //그리드 표시 관련 신호

        public MapData MapData = new MapData(); // 레지스터.  데이터 갱신은 MiR_Get_Register()함수 이용한다.
        public override string ToString()
        {

            return $"id={Id,-5}, " +
                   $"Floor={FloorName,-5}, " +
                   $"MapID={MapID,-5}, " +
                   $"DisplayFlag={DisplayFlag,-5}";
        }
    }
    public class MapData
    {
        public string MapViewName { get; set; }
        public float mapScale { get; set; }
        public Point mouseFirstLocation { get; set; }
        public Point mouseMoveOffset { get; set; }
    }
}
