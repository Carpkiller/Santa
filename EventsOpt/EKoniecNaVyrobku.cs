using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EKoniecNaVyrobku : UdalostOpt
    {
        private Elf _worker;
        private bool Opti;

        public EKoniecNaVyrobku(Elf worker, Hracka hracka, double koniec, bool opt)
        {
            Hracka = hracka;
            _worker = worker;
            SetCas(koniec);
            Opti = opt;
        }

        public override void Vykonaj(JadroOpt jadro)
        {
            if (Opti)
            {
                jadro.UvolniWorkera(_worker, Hracka);
            }
            else
            {
                jadro.UvolniWorkeraPrehadzovane(_worker, Hracka);
            }
            
        }
    }
}
