using System.Collections.Generic;
using System.Linq;
using Santa.Abstract;
using Santa.Comparers;

namespace Santa.Jadro
{
    public class KalendarUdalostiOpt : PriorFrontEventsOpt
    {
        private PriorFrontEventsOpt Kalendar;
    }

    public class PriorFrontEventsOpt {

        readonly SortedList<double, UdalostOpt> _priorFront;

        public PriorFrontEventsOpt()
        {
            _priorFront = new SortedList<double, UdalostOpt>(new ComparerEvents<double>());
        }

        public void Vloz(UdalostOpt p, double simCas)
        {
            var priorita = simCas;
            _priorFront.Add(priorita, p);
        }

        public UdalostOpt Zmaz()
        {
            UdalostOpt p = _priorFront.First().Value;
            _priorFront.RemoveAt(0);
            return p;
        }

        public bool IsEmpty()
        {
            return _priorFront.Count == 0;
        }
    }
}
