using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EPrekonvertujHracky : UdalostOpt
    {
        public EPrekonvertujHracky()
        {
            
        }

        public override void Vykonaj(JadroOpt jadro)
        {
            jadro.PrekonvertujHracky();
        }
    }
}
