﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Common
{
    public class PositionWaitTimeHistory
    {
        public int Id { get; set; }
        public string RobotName { get; set; }
        public string RobotAlias { get; set; }
        public string PositionName { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime FinishTime { get; set; }
        public string ElapsedTime { get; set; }   //경과시간

        public override string ToString()
        {
            return $"id={Id,-5}, " +
                   $"RobotName={RobotName,-5}, " +
                   $"RobotAlias={RobotAlias,-5}, " +
                   $"PositionName={PositionName,-5}, " +
                   $"CreateTime={CreateTime,-5}, " +
                   $"FinishTime={FinishTime,-5}, " +
                   $"ElapsedTime={ElapsedTime,-5}";
        }

    }
}
