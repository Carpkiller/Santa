using System;
using Santa.Abstract;
using Santa.Jadro;

namespace Santa.Events
{
    public class EKoniecPraceNaVyrobku : Udalost
    {
        private Elf _worker;

        public EKoniecPraceNaVyrobku(Elf worker, Hracka hracka, double koniec)
        {
            Hracka = hracka;
            _worker = worker;
            SetCas(koniec);
        }
        public override void Vykonaj(Jadro.Jadro jadro)
        {
            //Console.WriteLine("Koniec prace na vyrobky , elfId : "+_worker.Id +"  , hrackaId : "+Hracka.Id+" , cas : "+DateTime.FromOADate(GetCas()).ToString());
            jadro.UvolniWorkera(_worker, Hracka);
        }
    }
}
