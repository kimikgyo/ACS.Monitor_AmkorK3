using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monitor.Common
{
    public class CustomMapModel
    {
        public int Id { get; set; }
        public DateTime UpdateTime { get; set; }
        public string MapName { get; set; }
        public string MapImageData { get; set; }
        public override string ToString()
        {

            return $"id={Id,-5}, " +
                   $"UpdateTime={UpdateTime,-5}, " +
                   $"MapName={MapName,-5}, " +
                   $"MapImageData={MapImageData,-5}";
        }
    }
}
