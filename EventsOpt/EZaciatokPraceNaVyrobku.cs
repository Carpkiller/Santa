using System;
using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EZaciatokPraceNaVyrobku : UdalostOpt
    {
        private Elf _worker;

        public EZaciatokPraceNaVyrobku(Elf worker, Hracka hracka, double simCas)
        {
            Hracka = hracka;
            _worker = worker;
            _worker.ZaciatokPRace = DateTime.FromOADate(simCas);
            SetCas(simCas);
        }

        public override void Vykonaj(JadroOpt jadro)
        {
            jadro.NaplanujKoniecPrace(_worker, Hracka);
        }
    }
}
