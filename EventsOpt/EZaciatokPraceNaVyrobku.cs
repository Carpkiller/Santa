using System;
using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EZaciatokPraceNaVyrobku : UdalostOpt
    {
        private Elf _worker;
        private bool Opti;

        public EZaciatokPraceNaVyrobku(Elf worker, Hracka hracka, double simCas, bool opt)
        {
            Hracka = hracka;
            _worker = worker;
            _worker.ZaciatokPRace = DateTime.FromOADate(simCas);
            SetCas(simCas);
            Opti = opt;
        }

        public override void Vykonaj(JadroOpt jadro)
        {
            if (Opti)
            {
                //Console.WriteLine(DateTime.Now +"  "+Hracka.Id);
                jadro.NaplanujKoniecPrace(_worker, Hracka);
            }
            else
            {
                jadro.NaplanujKoniecPracePre(_worker, Hracka);
            }
        }
    }
}
