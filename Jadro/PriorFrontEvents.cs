using System.Collections.Generic;
using System.Linq;
using Santa.Abstract;
using Santa.Comparers;

namespace Santa.Jadro
{
    public class PriorFrontEvents
    {
        private readonly SortedList<double, Udalost> _priorFront;

        public PriorFrontEvents()
        {
            _priorFront = new SortedList<double, Udalost>(new ComparerEvents<double>());
        }

        public void Vloz(Udalost p, double simCas)
        {
            var priorita = simCas;
            _priorFront.Add(priorita, p);
        }

        public Udalost Zmaz()
        {
            Udalost p = _priorFront.First().Value;
            _priorFront.RemoveAt(0);
            return p;
        }

        public bool IsEmpty()
        {
            return _priorFront.Count == 0;
        }
    }
}
