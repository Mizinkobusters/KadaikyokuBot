using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KadaikyokuBot
{
    public class GakkyokuFormatter
    {

        public Fumen gakkyokuToFumen(Gakkyoku.Rootobject rootobject, Gakkyoku.Diff diff)
        {
            Fumen fumen = new Fumen();

            fumen.rootobject = rootobject; 
            fumen.diff = diff;

            return fumen;
        }

        public List<Fumen> gakkyokuListToFumenList(List<Gakkyoku.Rootobject> gakkyokuList)
        {
            List<Fumen> fumenList = Fumen.fumenList;

            for (int i = 0; i < gakkyokuList.Count; i++) 
            {
                if (!fumenList.Contains(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.bas))
                    && gakkyokuList[i].data.bas != null)
                {
                    fumenList.Add(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.bas));
                }
                if (!fumenList.Contains(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.adv))
                    && gakkyokuList[i].data.adv != null)
                {
                    fumenList.Add(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.adv));
                }
                if (!fumenList.Contains(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.exp))
                    && gakkyokuList[i].data.exp != null)
                {
                    fumenList.Add(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.exp));
                }
                if (!fumenList.Contains(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.mas))
                    && gakkyokuList[i].data.mas != null)
                {
                    fumenList.Add(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.mas));
                }
                if (!fumenList.Contains(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.ult))
                    && gakkyokuList[i].data.ult != null)
                {
                    fumenList.Add(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.ult));
                }
                if (!fumenList.Contains(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.we))
                    && gakkyokuList[i].data.we != null)
                {
                    fumenList.Add(gakkyokuToFumen(gakkyokuList[i], gakkyokuList[i].data.we));
                }
            }
            return fumenList;
        }
    }
}
