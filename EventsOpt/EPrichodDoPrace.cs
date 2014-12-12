using Santa.Abstract;
using Santa.Jadro;

namespace Santa.EventsOpt
{
    public class EPrichodDoPrace : UdalostOpt
    {
        private Elf _elf;
        private bool Opti;
        public EPrichodDoPrace(double prichod, Elf elf, bool opti)
        {
            _elf = elf;
            SetCas(prichod);
            Opti = opti;
        }

        public override void Vykonaj(JadroOpt jadro)
        {
            if (Opti)
            {
                jadro.PrichodElfaDoPrace(_elf);
            }
            else
            {
                jadro.PrichodElfaDoPracePrehadzovane(_elf);
            }
            
        }
    }
}
