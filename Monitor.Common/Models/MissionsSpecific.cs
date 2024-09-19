using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Common
{
    public class MissionsSpecific
    {
        public int No { get; set; }
        public string RobotAlias { get; set; }
        public string RobotName { get; set; }
        public string CallName { get; set; }
        public string CallState { get; set; }
        public string JobSection { get; set; }
        public DateTime CallTime { get; set; }
        public string Cancel { get; set; }
        public int Priority { get; set; }

        public override string ToString()
        {
            return $"No={No}, " +
                   $"RobotAlias={RobotAlias}, " +
                   $"RobotName={RobotName}, " +
                   $"CallName={CallName}, " +
                   $"CallState={CallState}, " +
                   $"JobSection={JobSection}, " +
                   $"CallTime={CallTime}, " +
                   $"Cancel={Cancel}, " +
                   $"Priority={Priority}";
        }
    }
}
