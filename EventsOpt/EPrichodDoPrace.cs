using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EPrichodDoPrace : UdalostOpt
    {
        private Elf _elf;
        public EPrichodDoPrace(double prichod, Elf elf)
        {
            _elf = elf;
            SetCas(prichod);
        }

        public override void Vykonaj(JadroOpt jadro)
        {
            jadro.PrichodElfaDoPrace(_elf);
        }
    }
}
