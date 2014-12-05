using Santa.Jadro;

namespace Santa.Abstract
{
    public abstract class Udalost : PrvokSystemu
    {
        public Hracka Hracka { get; set; }

        public abstract void Vykonaj(Jadro.Jadro jadro);
    }
}
