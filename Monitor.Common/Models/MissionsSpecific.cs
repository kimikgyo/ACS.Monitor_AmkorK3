using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Common
{
    public class MissionsSpecific
    {
        public int Id { get; set; }
        public string RobotGroup { get; set; }
        public string RobotName { get; set; }
        public string CallName { get; set; }
        public bool CallState { get; set; }
        public bool ACSState { get; set; }

        public override string ToString()
        {
            return $"Id={Id}, " +
                   $"RobotGroup={RobotGroup}, " +
                   $"RobotName={RobotName}, " +
                   $"CallName={CallName}, " +
                   $"CallState={CallState}, " +
                   $"ACSState={ACSState}";
        }
    }
}
