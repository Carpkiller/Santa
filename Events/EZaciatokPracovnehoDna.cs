using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Santa.Abstract;

namespace Santa.Events
{
    public class EZaciatokPracovnehoDna : Udalost
    {

        public EZaciatokPracovnehoDna(double simCas)
        {
            var date = DateTime.FromOADate(simCas);
            SetCas(simCas);
        }
        public override void Vykonaj(Jadro.Jadro jadro)
        {
            jadro.SpustVyrobu();
        }
    }
}
