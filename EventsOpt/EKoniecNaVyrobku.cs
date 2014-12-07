using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EKoniecNaVyrobku : UdalostOpt
    {
        private Elf _worker;

        public EKoniecNaVyrobku(Elf worker, Hracka hracka, double koniec)
        {
            Hracka = hracka;
            _worker = worker;
            SetCas(koniec);
        }

        public override void Vykonaj(JadroOpt jadro)
        {
            jadro.UvolniWorkera(_worker, Hracka);
        }
    }
}
