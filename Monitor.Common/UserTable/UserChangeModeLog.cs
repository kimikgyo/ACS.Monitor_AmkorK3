using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Common
{
    public class UserChangeModeLog
    {
        //Elevator Mode 변경 대한 History

        public int Id { get; set; }                
        public string UserNumber { get; set; }      // 사번
        public string UserName { get; set; }
        public string ChangeModeLog { get; set; }   // Log 내용
        public DateTime CreateTime { get; set; }    // Mode 변경시간

        public override string ToString()
        {

            return $"id={Id,-5}, " +
                   $"UserNumber={UserNumber,-5}, " +
                   $"UserName={UserName,-5}, " +
                   $"ChangeModeLog={ChangeModeLog,-5}, " +
                   $"CreateTime={CreateTime,-5}";
        }

    }
}
