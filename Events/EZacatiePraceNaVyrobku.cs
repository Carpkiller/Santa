using System;
using Santa.Abstract;
using Santa.Jadro;

namespace Santa.Events
{
    public class EZacatiePraceNaVyrobku : Udalost
    {
        private Elf _worker;

        public EZacatiePraceNaVyrobku(Elf worker, Hracka hracka, double simCas)
        {
            Hracka = hracka;
            _worker = worker;
            _worker.ZaciatokPRace = DateTime.FromOADate(simCas);
            SetCas(simCas);
        }
        public override void Vykonaj(Jadro.Jadro jadro)
        {
            //Console.WriteLine("Zacatie prace na vyrobky , elfId : " + _worker.Id + "  , hrackaId : " + Hracka.Id + "  , cas : " + DateTime.FromOADate(GetCas()).ToString());
            jadro.NaplanujKoniecPrace(_worker, Hracka);
        }
    }
}
