using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadaikyokuBot
{
    public class Fumen
    {
        public static List<Fumen> fumenList = new List<Fumen>();

        public Gakkyoku.Rootobject rootobject { get; set; }
        public Gakkyoku.Diff diff { get; set; }
    }
}
