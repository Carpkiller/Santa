using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EPrichodNovehoVyrobku : UdalostOpt
    {
        public EPrichodNovehoVyrobku(Hracka hracka, double simCas)
        {
            Hracka = hracka;
            SetCas(hracka.PrichodDoSystemu);
        }


        public override void Vykonaj(JadroOpt jadro)
        {
            jadro.NaplanujPrichodNovehoVyrobku();

            //jadro.ZacniPracuNaVyrobku(Hracka);
        }
    }
}
