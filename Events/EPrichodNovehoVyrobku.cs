using System;
using Santa.Abstract;
using Santa.Jadro;

namespace Santa.Events
{
    public class EPrichodNovehoVyrobku : Udalost
    {
        private bool _jeStary;
        public EPrichodNovehoVyrobku(Hracka hracka, bool stary, double simCas)
        {
            Hracka = hracka;
            SetCas(hracka.PrichodDoSystemu);
            _jeStary = stary;
        }

        public override void Vykonaj(Jadro.Jadro jadro)
        {
            //Console.WriteLine("Prochod noveho vyrobku Id : "+Hracka.Id);
            jadro.NaplanujPrichodNovehoVyrobku();
            
            jadro.ZacniPracuNaVyrobku(Hracka);
        }
    }
}
