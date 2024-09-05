using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Common
{
   public class UserNumberModel
    {
        //============================================
        public int Id { get; set; }                 
        public string UserNumber { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }      //User 비밀번호(사용X)
        public int CallMissionAuthority { get; set; } //모니터링 Call권한부여
        public int ElevatorAuthority { get; set; }    //Elevator 권한부여
        public int DisplayFlag { get; set; }
    }
}
