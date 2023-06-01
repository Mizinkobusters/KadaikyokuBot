using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadaikyokuBot
{
    public class KadaikyokuCondition
    {
        public KadaikyokuCondition (double minLevel, double maxLevel)
        {
            minLevel = this.minLevel;
            maxLevel = this.maxLevel;
        }

        public double minLevel { get; set; } = 0;
        public double maxLevel { get; set; } = 0;
        public string[] genres { get; set; }
        public string[] difficultys { get; set; }
    }
}
