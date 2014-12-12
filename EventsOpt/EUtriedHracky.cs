using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EUtriedHracky : UdalostOpt
    {
        public override void Vykonaj(JadroOpt jadro)
        {
            jadro.UtriedHracky();
        }
    }
}
